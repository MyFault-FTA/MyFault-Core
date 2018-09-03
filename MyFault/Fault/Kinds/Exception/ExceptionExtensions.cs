namespace MyFault.Fault.Kinds.Exception
{
    public static class ExceptionExtensions
    {
        public static IFaultChain AsException(this FaultChain chain, System.Exception ex)
        {
            return chain.Fault(new ExceptionFault(ex));
        }
            
    }
}