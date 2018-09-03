using System;
using System.Collections.Generic;
using MyFault.Fault.Instance;

namespace MyFault.Fault
{
    public abstract class Fault
    {
        public string Message { get; }
        public FaultInstance Instance { get; }
        private DateTime CreatedTime { get; }
        
        

        protected Fault(string message)
        {
            Message = message;
            CreatedTime = DateTime.UtcNow;
            Instance = new FaultInstance();
        }

        public abstract IEnumerable<object> ProvideHashData();

        public string GetFaultKind()
        {
            return this.GetType().FullName;
        }
    }
}