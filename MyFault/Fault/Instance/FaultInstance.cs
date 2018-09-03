using System;
using System.Linq.Expressions;
using MyFault.Fault.Instance.Data;
using MyFault.Fault.Instance.Data.Binary;

namespace MyFault.Fault.Instance
{
    public class FaultInstance
    {
        public DateTime CreatedTime { get; }
        public Guid Id { get; }

        public InstanceDataCollection Data { get; }
        public BinaryInstanceDataCollection BinaryData { get; }

        public FaultInstance()
        {
            Data = new InstanceDataCollection();
            BinaryData = new BinaryInstanceDataCollection();
            CreatedTime = DateTime.UtcNow;
            Id = Guid.NewGuid();
        }

        public FaultInstance Having(string key, string value)
        {
            Data.Add(key, new InstanceData(InstanceDataType.Value, key, value));
            return this;
        }

        public FaultInstance HavingData<T>(Expression<Func<T>> memberExpression, int maxDepth = 2)
        {
            string objName = ((MemberExpression) memberExpression.Body).Member.Name;
            T obj = memberExpression.Compile()();
            Data.Add(objName, new InstanceData(InstanceDataType.Object, objName, obj.ToString()));
            return this;
        }

        public FaultInstance Having(string name, byte[] content, string contentType)
        {
            BinaryData.Add(name, new BinaryInstanceData(name, content, contentType));
            return this;
        }
    }
}