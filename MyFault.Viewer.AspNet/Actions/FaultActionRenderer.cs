using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MyFault.Data;
using MyFault.Data.Model;
using MyFault.MsSql;
using MyFault.Viewer.AspNet.Util;

namespace MyFault.Viewer.AspNet.Actions
{
    public class FaultActionRenderer : IActionRenderer
    {
        public object RenderResponse(HttpContext context)
        {
            IFaultQueryDataProvider queryDataProvider =
                new MsSqlQueryDataProvider(
                    new SqlConnection("Data Source=APP;Integrated Security=True;Database=MYFAULT_DEV"));

            TimeSpan window = WindowSpanParser.ParseWindowSpan(context.Request.QueryString);
            List<FaultSummary> faultSummaries = queryDataProvider.GetFaultSummaries(window);
            return faultSummaries;
        }
    }
}