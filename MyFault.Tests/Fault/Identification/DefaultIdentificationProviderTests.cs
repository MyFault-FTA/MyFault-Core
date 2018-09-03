using System.Threading.Tasks;
using MyFault.Fault.Identification;
using MyFault.Fault.Identification.Default;
using MyFault.Fault.Kinds;
using MyFault.Fault.Kinds.Message;
using NUnit.Framework;
#pragma warning disable 1998

namespace MyFault.Tests.Fault.Identification
{
    [TestFixture]
    public class DefaultIdentificationProviderTests
    {
        [Test]
        public async Task CombineData_ShouldConcatenateStrings_GivenListOfTwoStrings()
        {
            DefaultHasher identifier = new DefaultHasher();
            string expected = "string1string2";
            string actual = identifier.CombineData(new[] {"string1", "string2"});

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task Identify_ShouldGenerateSha256Hash_GivenAnyString()
        {
            DefaultHasher identificationprovider = new DefaultHasher();
            string expected = "2f2rMO7F4839hhP3QL2g26ej2SIGlw0GKTxa6F0/NIk=";

            MessageFault fault = new MessageFault("Test Message");
            string actual = identificationprovider.GetFaultHash(fault).Data;

            Assert.AreEqual(expected, actual);
        }
    }
}