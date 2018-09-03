using System.Collections.Generic;

namespace MyFault.Fault.Kinds.Message
{
    public class MessageFault : Fault
    {
        public MessageFault(string message) : base(message)
        {
        }

        public override IEnumerable<object> ProvideHashData()
        {
            return new[] {Message};
        }
    }
}