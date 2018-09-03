using System;
using System.Collections.Generic;
using MyFault.Data;
using MyFault.Fault.Identification;
using MyFault.Fault.Identification.Default;
using MyFault.Fault.Instance.Data.Collector;

namespace MyFault.Configuration
{
    public partial class MyFaultConfig
    {
        private IFaultEntryDataProvider _entryDataProvider;
        private IFaultHasher _identififactionProvider = new DefaultHasher();
        private List<IDataCollector> _dataCollectors = new List<IDataCollector>();

        public IFaultEntryDataProvider EntryDataProvider
        {
            get { return _entryDataProvider; }
        }

        public IFaultHasher IdentififactionProvider
        {
            get { return _identififactionProvider; }
        }

        public List<IDataCollector> DataCollectors
        {
            get { return _dataCollectors; }
        }

        public void WithDataProvider(IFaultEntryDataProvider entryDataProvider)
        {
            _entryDataProvider = entryDataProvider;
        }

        public void WithIdentificationProvider(IFaultHasher hasher)
        {
            _identififactionProvider = IdentififactionProvider;
        }

        public void WithDataCollector(IDataCollector collector)
        {
            DataCollectors.Add(collector);
        }
    }

    /// <summary>
    /// Static members
    /// </summary>
    public partial class MyFaultConfig
    {
        private static MyFaultConfig _defaultConfiguration;

        public static MyFaultConfig CurrentDefaults
        {
            get
            {
                if (_defaultConfiguration == null)
                    _defaultConfiguration = new MyFaultConfig();
                return _defaultConfiguration;
            }
        }
    }
}