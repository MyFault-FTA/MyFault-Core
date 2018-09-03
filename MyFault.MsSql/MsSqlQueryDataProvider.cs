using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public class MsSqlQueryDataProvider : IFaultQueryDataProvider
    {
        private readonly IDbConnection _connection;

        public MsSqlQueryDataProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        public List<FaultSummary> GetFaultSummaries(TimeSpan window)
        {
            DateTime startDate = DateTime.UtcNow - window;
            return _connection.Query<FaultSummary>(@"
                SELECT f.faultid,
                        f.Kind,
                        f.FaultHash as Hash, 
                       f.message, 
                       rn.createdtime as LastInstanceTime, 
                       (SELECT Count(*) 
                        FROM   instance 
                        WHERE  instance.faultid = f.faultid) as InstanceCount
                FROM   fault f 
                       INNER JOIN (SELECT faultid, 
                                          createdtime, 
                                          Row_number() 
                                            OVER ( 
                                              partition BY faultid 
                                              ORDER BY createdtime DESC ) RowNum 
                                   FROM   instance
                                   WHERE  CreatedTime > @StartDate) rn 
                               ON rn.faultid = f.faultid 
                WHERE  rn.rownum = 1 
                ORDER  BY LastInstanceTime DESC ",
                new {StartDate = startDate}).ToList();
        }

        public List<FaultInstanceSummary> GetInstanceSummaries(Guid faultId, TimeSpan window)
        {
            DateTime startDate = DateTime.UtcNow - window;
            return _connection.Query<FaultInstanceSummary>(@"
               SELECT i.instanceid as InstanceId, 
                           i.createdtime as InstanceTime, 
                           (SELECT Count(*) 
                            FROM   instancedata 
                            WHERE  instanceid = i.instanceid) AS DataCount, 
                           (SELECT Count(*) 
                            FROM   instancedatabinary 
                            WHERE  instanceid = i.instanceid) AS BinaryDataCount 
                    FROM   [MYFAULT_DEV].[dbo].[instance] i 
                    WHERE i.CreatedTime > @StartDate
                    AND i.FaultId = @FaultId",
                new
                {
                    StartDate = startDate,
                    FaultId = faultId
                }).ToList();
        }

        public List<InstanceDataModel> GetData(Guid instanceId)
        {
            return _connection.Query<InstanceDataModel>(@"
                   SELECT [DataTypeId]
                          ,[DataKey] as [Key]
                          ,[DataValue] as [Value]
                      FROM [MYFAULT_DEV].[dbo].[InstanceData]
                  WHERE InstanceId = @InstanceId",
                new
                {
                    InstanceId = instanceId
                }).ToList();
        }
    }
}