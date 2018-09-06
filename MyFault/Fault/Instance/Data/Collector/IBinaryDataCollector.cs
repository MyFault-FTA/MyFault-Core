using System.Collections.Generic;
using MyFault.Fault.Instance.Data.Binary;

namespace MyFault.Fault.Instance.Data.Collector
{
    public interface IBinaryDataCollector
    {
        List<BinaryInstanceData> CollectData();
    }
}