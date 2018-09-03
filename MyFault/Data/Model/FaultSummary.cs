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

    public class FaultInstanceSummary
    {
        public Guid InstanceId { get; set; }
        public DateTime InstanceTime { get; set; }
        public int DataCount { get; set; }
        public int BinaryDataCount { get; set; }
    }
}