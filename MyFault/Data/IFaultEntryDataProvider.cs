using System;
using System.Collections.Generic;
using MyFault.Data.Model;
using MyFault.Fault.Identification;
using MyFault.Fault.Instance;

namespace MyFault.Data
{
    public interface IFaultEntryDataProvider
    {
        Guid GetFaultIdByHash(FaultHash fault);
        Guid CreateFault(FaultHash hash, Fault.Fault fault);
        void CreateFaultInstance(Guid faultId, FaultInstance instance);
    }
}