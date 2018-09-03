using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MyFault.Fault.Identification.Default
{
    public class DefaultHasher : IFaultHasher
    {
        public FaultHash GetFaultHash(Fault fault)
        {
            string identifierData = CombineData(fault.ProvideHashData());
            UnicodeEncoding encoding = new UnicodeEncoding();
            byte[] clear = encoding.GetBytes(identifierData);
            SHA256Managed sha = new SHA256Managed();
            byte[] bytes = sha.ComputeHash(clear);
            string hashString = Convert.ToBase64String(bytes);
            return new FaultHash {Data = hashString};
        }

        public string CombineData(IEnumerable<object> hashData)
        {
            return string.Join(string.Empty, hashData);
        }
    }
}