using System;
using System.Linq.Expressions;

namespace MyFault
{
    public interface IFaultChain
    {
        IFaultChain Fault(Fault.Fault fault);
        IFaultChain Having(string key, string value);
        IFaultChain Having<T>(Expression<Func<T>> memberExpression, int maxDepth = 2);
        IFaultChain Having(string name, byte[] content, string contentType);
        IFaultChain Having(string name, string filePath, string contentType);
        Fault.Fault Handle();
    }
}