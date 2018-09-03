using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Web;
using MyFault.Data;
using MyFault.Data.Model;
using MyFault.MsSql;
using MyFault.Viewer.AspNet.Actions;

namespace MyFault.Viewer.AspNet
{
    internal class DataActionRenderer : IActionRenderer
    {
        public object RenderResponse(HttpContext context)
        {
            IFaultQueryDataProvider queryDataProvider =
                new MsSqlQueryDataProvider(
                    new SqlConnection("Data Source=APP;Integrated Security=True;Database=MYFAULT_DEV"));
            Guid instanceId = GetInstanceId(context.Request.QueryString);
            List<InstanceDataModel> models = queryDataProvider.GetData(instanceId);
            return models;
        }

     private Guid GetInstanceId(NameValueCollection requestQueryString)
        {
            Guid instanceId = Guid.Parse(requestQueryString["instanceId"]);
            return instanceId;
        }
    }
}