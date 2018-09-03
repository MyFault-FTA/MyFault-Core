using System;
using System.Data.SqlClient;
using System.Dynamic;
using System.Net.Mime;
using MyFault.Configuration;
using MyFault.Data;
using MyFault.Fault.Kinds;
using MyFault.Fault.Kinds.Exception;
using MyFault.MsSql;
using MyFault.MsSql.Configuration;
using NUnit.Framework;

namespace MyFault.Tests
{
    [TestFixture]
    public class MyFaultTests
    {
        [Test]
        public void TestMain()
        {
            MyFaultConfig.CurrentDefaults.WithMsSqlData("Data Source=APP;Integrated Security=True;Database=MYFAULT_DEV");
            
            try
            {
                throw new ApplicationException("New Exception");
            }
            catch (Exception exception)
            {
                var state = new {Message = "Somethign went wrong", Type="Error"};
                MyFaultHandler.Current.New().AsException(exception)
                    .Having("TestKey", "TestVal")
                    .Having(() => state)
                    .Having(()=> Environment.UserName)
                    .Having(() => Environment.MachineName)
                    .Having("hello.txt", "C:\\New Folder\\Dapper.dll", "application\\text")
                .Handle();
            }
        }
    }
}