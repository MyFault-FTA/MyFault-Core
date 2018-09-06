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
        public IFaultHasher IdentififactionProvider { get; private set; } = new DefaultHasher();
        public IFaultEntryDataProvider EntryDataProvider { get; private set; }
        public List<IDataCollector> DataCollectors { get; } = new List<IDataCollector>();
        public List<IBinaryDataCollector> BinaryDataCollectors { get; } = new List<IBinaryDataCollector>();

        public MyFaultConfig WithDataProvider(IFaultEntryDataProvider entryDataProvider)
        {
            EntryDataProvider = entryDataProvider;
            return this;
        }

        public MyFaultConfig WithIdentificationProvider(IFaultHasher hasher)
        {
            IdentififactionProvider = IdentififactionProvider;
            return this;
        }

        public MyFaultConfig WithCollector(IDataCollector collector)
        {
            DataCollectors.Add(collector);
            return this;
        }
        
        public MyFaultConfig WithBinaryCollector(IBinaryDataCollector collector)
        {
            BinaryDataCollectors.Add(collector);
            return this;
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