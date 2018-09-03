using System;
using System.Collections.Generic;
using MyFault.Data.Model;

namespace MyFault.Data
{
    public interface IFaultQueryDataProvider
    {
        List<FaultSummary> GetFaultSummaries(TimeSpan window);
        List<FaultInstanceSummary> GetInstanceSummaries(Guid faultId, TimeSpan window);
        List<InstanceDataModel> GetData(Guid instanceId);
    }
}