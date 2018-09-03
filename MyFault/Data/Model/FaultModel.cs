using System;
using System.Collections.Generic;

namespace MyFault.Data.Model
{
    public class FaultModel
    {
        public string FaultId { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string Kind { get; set; }
        public DateTime CreatedDate { get; set; }

        public List<InstanceModel> Instances { get; set; }

    }
}