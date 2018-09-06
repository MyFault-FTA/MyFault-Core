namespace MyFault.Fault.Instance.Data
{
    public class InstanceData
    {
        public string DataKey { get; set; }
        public string DataValue { get; }
        public InstanceDataType DataType { get; }

        public InstanceData(InstanceDataType dataType, string dataKey, string dataValue)
        {
            DataType = dataType;
            DataKey = dataKey;
            DataValue = dataValue;
        }
    }
}