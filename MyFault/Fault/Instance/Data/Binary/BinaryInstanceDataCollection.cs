using System.Collections.Generic;

namespace MyFault.Fault.Instance.Data.Binary
{
    public class BinaryInstanceDataCollection : Dictionary<string, BinaryInstanceData>
    {
        public void AddMany(IEnumerable<BinaryInstanceData> data)
        {
            foreach (BinaryInstanceData binaryInstanceData in data)
            {
                this[binaryInstanceData.DataKey] = binaryInstanceData;
            }
        }
    }
    
    
}