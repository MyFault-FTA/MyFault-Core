using System;
using System.Collections.Generic;

namespace MyFault.Fault.Instance.Data.Collector.Defaults
{
    public class WindowsVersionCollector : IDataCollector
    {
        public string DefineDataKeyPrefix() => "WinVersion";
        
        public IEnumerable<InstanceData> CollectData()
        {
         return new List<InstanceData>
         {
             new InstanceData(InstanceDataType.Value, "WindowsVersion", Environment.OSVersion.VersionString)
         };
        }
    }
}