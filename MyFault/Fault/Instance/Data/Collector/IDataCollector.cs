using System;
using System.Collections.Generic;

namespace MyFault.Fault.Instance.Data.Collector
{
    public interface IDataCollector
    {
        string DefineDataKeyPrefix();
        IEnumerable<InstanceData> CollectData();
    }
}