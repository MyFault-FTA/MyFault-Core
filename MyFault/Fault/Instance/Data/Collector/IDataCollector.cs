using System.Collections.Generic;

namespace MyFault.Fault.Instance.Data.Collector
{
    public interface IDataCollector
    {
        List<InstanceData> CollectData();

    }
}