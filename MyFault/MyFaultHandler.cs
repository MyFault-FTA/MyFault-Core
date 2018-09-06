using System;
using System.Collections.Generic;
using MyFault.Configuration;
using MyFault.Data;
using MyFault.Fault;
using MyFault.Fault.Identification;
using MyFault.Fault.Instance;
using MyFault.Fault.Instance.Data;
using MyFault.Fault.Instance.Data.Binary;
using MyFault.Fault.Instance.Data.Collector;

namespace MyFault
{
    public sealed partial class MyFaultHandler
    {
        private readonly IFaultEntryDataProvider _entryDataProvider;
        private readonly IFaultHasher _faultHasher;
        private readonly List<IDataCollector> _dataCollectors;
        private readonly List<IBinaryDataCollector> _binaryDataCollectors;

        public MyFaultHandler(IFaultEntryDataProvider entryDataProvider,
            IFaultHasher faultHasher,
            List<IDataCollector> dataCollectors,
            List<IBinaryDataCollector> binaryDataCollectors)
        {
            _entryDataProvider = entryDataProvider;
            _faultHasher = faultHasher;
            _dataCollectors = dataCollectors;
            _binaryDataCollectors = binaryDataCollectors;
        }

        public FaultChain New()
        {
            return new FaultChain(this);
        }

        public void Handle(Fault.Fault fault)
        {
            AddCollectedData(fault.Instance.Data);
            AddCollectedBinaryData(fault.Instance.BinaryData);
            FaultHash hash = _faultHasher.GetFaultHash(fault);

            Guid faultId = _entryDataProvider.GetFaultIdByHash(hash);
            if (faultId == Guid.Empty)
                faultId = _entryDataProvider.CreateFault(hash, fault);

            _entryDataProvider.CreateFaultInstance(faultId, fault.Instance);
        }

        private void AddCollectedData(InstanceDataCollection instanceData)
        {
            foreach (var collector in _dataCollectors)
            {
                IEnumerable<InstanceData> instanceDatas = collector.CollectData();
                foreach (var instanceDataPair in instanceDatas)
                {
                    string newKey = CompileCollectorDataKey(collector.DefineDataKeyPrefix(), instanceDataPair.DataKey);
                    instanceDataPair.DataKey = newKey;
                    instanceData.Add(newKey, instanceDataPair);
                }
            }
        }

        private string CompileCollectorDataKey(string defineDataKeyPrefix, string key)
        {
            if (string.IsNullOrEmpty(defineDataKeyPrefix))
                return key;
            return $"{defineDataKeyPrefix}:{key}";
        }

        private void AddCollectedBinaryData(BinaryInstanceDataCollection binaryInstanceData)
        {
            _binaryDataCollectors.ForEach(x => binaryInstanceData.AddMany(x.CollectData()));
        }
    }

    /// <summary>
    /// Static members
    /// </summary>
    public sealed partial class MyFaultHandler
    {
        private static MyFaultHandler _globalHandler;

        public static MyFaultHandler Current
        {
            get
            {
                if (_globalHandler == null)
                    _globalHandler = new MyFaultHandler(
                        MyFaultConfig.CurrentDefaults.EntryDataProvider,
                        MyFaultConfig.CurrentDefaults.IdentififactionProvider,
                        MyFaultConfig.CurrentDefaults.DataCollectors,
                        MyFaultConfig.CurrentDefaults.BinaryDataCollectors);
                return _globalHandler;
            }
        }
    }
}