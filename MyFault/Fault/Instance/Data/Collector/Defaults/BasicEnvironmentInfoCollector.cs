using System;
using System.Collections.Generic;

namespace MyFault.Fault.Instance.Data.Collector.Defaults
{
    public class BasicEnvironmentInfoCollector : IDataCollector
    {
        string IDataCollector.DefineDataKeyPrefix() => "BasicEnv";

        public IEnumerable<InstanceData> CollectData()
        {
            return new List<InstanceData>
            {
                new InstanceData(InstanceDataType.Value,"MachineHostName",Environment.MachineName),
                new InstanceData(InstanceDataType.Value, "MachineDomainName", Environment.UserDomainName),
                new InstanceData(InstanceDataType.Value, "MachineUserName", Environment.UserName)
            };
        }
    }
}