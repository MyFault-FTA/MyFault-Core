namespace MyFault.Fault.Instance.Data.Binary
{
    public class BinaryInstanceData
    {
        public string DataKey { get; }
        public byte[] DataContent { get; }
        public string ContentType { get; }

        public BinaryInstanceData(string dataKey, byte[] dataContent, string contentType)
        {
            DataContent = dataContent;
            ContentType = contentType;
            DataKey = dataKey;
        }
    }
}