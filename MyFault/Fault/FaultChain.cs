using System;
using System.IO;
using System.Linq.Expressions;

namespace MyFault.Fault
{
    public interface IFaultChain
    {
        IFaultChain Fault(Fault fault);
        IFaultChain Having(string key, string value);
        IFaultChain Having<T>(Expression<Func<T>> memberExpression, int maxDepth = 2);
        IFaultChain Having(string name, byte[] content, string contentType);
        IFaultChain Having(string name, string filePath, string contentType);
        void Handle();
    }

    public class FaultChain : IFaultChain
    {
        private readonly MyFaultHandler _handler;
        private Fault _fault;

        public FaultChain(MyFaultHandler handler)
        {
            _handler = handler;
        }

        public IFaultChain Fault(Fault fault)
        {
            _fault = fault;
            return this;
        }

        public IFaultChain Having(string key, string value)
        {
            _fault.Instance.Having(key, value);
            return this;
        }

        public IFaultChain Having<T>(Expression<Func<T>> memberExpression, int maxDepth = 2)
        {
            _fault.Instance.HavingData(memberExpression, maxDepth);
            return this;
        }

        public IFaultChain Having(string name, byte[] content, string contentType)
        {
            _fault.Instance.Having(name, content, contentType);
            return this;
        }

        public IFaultChain Having(string name, string filePath, string contentType)
        {
            _fault.Instance.Having(name, File.ReadAllBytes(filePath), contentType);
            return this;
        }

        public void Handle()
        {
            _handler.Handle(_fault);
        }
    }
}