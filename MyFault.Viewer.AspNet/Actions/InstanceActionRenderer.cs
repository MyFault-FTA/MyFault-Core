using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Web;
using MyFault.Data;
using MyFault.Data.Model;
using MyFault.MsSql;
using MyFault.Viewer.AspNet.Util;

namespace MyFault.Viewer.AspNet.Actions
{
    internal class InstanceActionRenderer : IActionRenderer
    {
        public object RenderResponse(HttpContext context)
        {
            IFaultQueryDataProvider queryDataProvider =
                new MsSqlQueryDataProvider(
                    new SqlConnection("Data Source=APP;Integrated Security=True;Database=MYFAULT_DEV"));

            TimeSpan window = WindowSpanParser.ParseWindowSpan(context.Request.QueryString);
            Guid faultId = GetFaultId(context.Request.QueryString);
            List<FaultInstanceSummary> faultInstanceSummaries = queryDataProvider.GetInstanceSummaries(faultId,window);
            return faultInstanceSummaries;
        }

        private Guid GetFaultId(NameValueCollection requestQueryString)
        {
            Guid faultId = Guid.Parse(requestQueryString["faultId"]);
            return faultId;
        }
    }
}