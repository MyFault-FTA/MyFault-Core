using System.Collections.Generic;

namespace MyFault.Fault.Instance.Data
{
    public class InstanceDataCollection : Dictionary<string, InstanceData>
    {
        public void AddMany(IEnumerable<InstanceData> data)
        {
            foreach (InstanceData binaryInstanceData in data)
            {
                this[binaryInstanceData.DataKey] = binaryInstanceData;
            }
        }
    }
}