using System;
using System.Collections.Generic;
using System.Dynamic;

namespace MyFault.Data.Model
{
    public class FaultSummary
    {
        public Guid FaultId { get; set; }
        public string Kind { get; set; }
        public string Hash { get; set; }
        public string Message { get; set; }
        public DateTime LastInstanceTime { get; set; }
        public int InstanceCount { get; set; }
    }
}