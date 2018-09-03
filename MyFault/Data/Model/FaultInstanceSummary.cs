using System;

namespace MyFault.Data.Model
{
    public class FaultInstanceSummary
    {
        public Guid InstanceId { get; set; }
        public DateTime InstanceTime { get; set; }
        public int DataCount { get; set; }
        public int BinaryDataCount { get; set; }
    }
}