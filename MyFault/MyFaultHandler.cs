using System;
using System.Collections.Generic;
using MyFault.Configuration;
using MyFault.Data;
using MyFault.Fault.Identification;
using MyFault.Fault.Instance;
using MyFault.Fault.Instance.Data.Binary;
using MyFault.Fault.Instance.Data.Collector;

namespace MyFault
{
    public sealed partial class MyFaultHandler
    {
        private readonly IFaultEntryDataProvider _entryDataProvider;
        private readonly IFaultHasher _faultHasher;
        private readonly List<IDataCollector> _dataCollectors;

        public MyFaultHandler(IFaultEntryDataProvider entryDataProvider,
            IFaultHasher faultHasher,
            List<IDataCollector> dataCollectors)
        {
            _entryDataProvider = entryDataProvider;
            _faultHasher = faultHasher;
            _dataCollectors = dataCollectors;
        }

        public FaultChain New()
        {
            return new FaultChain(this);
        }

        public void Handle(Fault.Fault fault)
        {
            AddCollectedData(fault.Instance.BinaryData);
            FaultHash hash = _faultHasher.GetFaultHash(fault);
            
            Guid faultId =_entryDataProvider.GetFaultIdByHash(hash);
            if(faultId == Guid.Empty)
                faultId =_entryDataProvider.CreateFault(hash, fault);
            
            _entryDataProvider.CreateFaultInstance(faultId, fault.Instance);
            
        }

        private void AddCollectedData(BinaryInstanceDataCollection instanceBinaryData)
        {
            throw new NotImplementedException();
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
                        MyFaultConfig.CurrentDefaults.DataCollectors);
                return _globalHandler;
            }
        }
    }
}