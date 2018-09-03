using System.Collections.Generic;

namespace MyFault.Fault.Kinds.Exception
{
    public class ExceptionFault : Fault
    {
        public System.Exception Exception { get; set; }

        public override IEnumerable<object> ProvideHashData()
        {
            return new List<object> {Exception.Message, Exception.StackTrace};
        }

        public ExceptionFault(System.Exception ex) : base(ex.Message)
        {
            Exception = ex;
        }
    }
}