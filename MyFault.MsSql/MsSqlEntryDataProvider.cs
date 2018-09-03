using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Instrumentation;
using System.Security.Cryptography.X509Certificates;
using Dapper;
using MyFault.Data;
using MyFault.Data.Model;
using MyFault.Fault.Identification;
using MyFault.Fault.Instance;
using MyFault.Fault.Instance.Data;
using MyFault.Fault.Instance.Data.Binary;

// ReSharper disable RedundantAnonymousTypePropertyName

namespace MyFault.MsSql
{
    public class MsSqlEntryDataProvider : IFaultEntryDataProvider
    {
        private readonly IDbConnection _connection;

        public MsSqlEntryDataProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        public Guid GetFaultIdByHash(FaultHash fault)
        {
            Guid id = _connection.ExecuteScalar<Guid>(
                "SELECT TOP 1 FaultId FROM Fault WHERE FaultHash=@FaultHash",
                new {FaultHash = fault.Data});

            return id;
        }

        public Guid CreateFault(FaultHash hash, Fault.Fault fault)
        {
            Guid newId = Guid.NewGuid();
            _connection.Execute(
                "INSERT INTO Fault (FaultId, FaultHash, Message, Kind, Data) " +
                "VALUES(@FaultId, @FaultHash, @Message, @Kind, @Data)",
                new
                {
                    FaultId = newId,
                    FaultHash = hash.Data,
                    Message = fault.Message,
                    Kind = fault.GetFaultKind(),
                    Data = string.Join(string.Empty, fault.ProvideHashData())
                });
            return newId;
        }

        public void CreateFaultInstance(Guid faultId, FaultInstance instance)
        {
            _connection.Execute(
                "INSERT INTO Instance(InstanceId, FaultId, CreatedTime) " +
                "VALUES(@InstanceId, @FaultId, @CreatedTime)",
                new
                {
                    InstanceId = instance.Id,
                    FaultId = faultId,
                    CreatedTime = instance.CreatedTime
                });
            SaveInstanceData(instance);
            SaveBinaryInstanceData(instance);
        }

        public FaultModel GetLastFault()
        {
            return _connection.QueryFirst<FaultModel>(@"
                SELECT TOP 1 Fault.*
                FROM Instance
                INNER JOIN Fault ON Instance.FaultId = Fault.FaultId
                ORDER BY instance.CreatedTime DESC ");
        }

        public IEnumerable<FaultModel> GetLastFaults(int take)
        {
            return _connection.Query<FaultModel>(@"
                SELECT TOP " + take + @" Fault.*
                FROM Instance
                INNER JOIN Fault ON Instance.FaultId = Fault.FaultId
                ORDER BY instance.CreatedTime DESC ");
        }

        private void SaveBinaryInstanceData(FaultInstance instance)
        {
            foreach (BinaryInstanceData binaryInstanceData in instance.BinaryData.Values)
            {
                _connection.Execute(
                    "INSERT INTO InstanceDataBinary (InstanceId, DataKey, DataContentType, DataContents) " +
                    "VALUES (@InstanceId, @Name, @ContentType, @Content)",
                    new
                    {
                        InstanceId = instance.Id,
                        Name = binaryInstanceData.DataKey,
                        ContentType = binaryInstanceData.ContentType,
                        Content = binaryInstanceData.DataContent
                    });
            }
        }

        private void SaveInstanceData(FaultInstance instance)
        {
            foreach (InstanceData instanceData in instance.Data.Values)
            {
                _connection.Execute(
                    "INSERT INTO InstanceData (InstanceId, DataTypeId, DataKey, DataValue) " +
                    "VALUES (@InstanceId, @TypeId, @DataKey, @DataValue)",
                    new
                    {
                        InstanceId = instance.Id,
                        TypeId = (int) instanceData.DataType,
                        DataKey = instanceData.DataKey,
                        DataValue = instanceData.DataValue
                    });
            }
        }
    }
}