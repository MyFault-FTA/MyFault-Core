using System.Collections.Generic;
using System.Web;
using MyFault.Fault.Instance.Data;
using MyFault.Fault.Instance.Data.Collector;

namespace MyFault.Viewer.AspNet.DataCollectors
{
    public class HttpContextDataCollector : IDataCollector
    {
        public string DefineDataKeyPrefix() => "HttpContext";

        public IEnumerable<InstanceData> CollectData()
        {
            List<InstanceData> newData = new List<InstanceData>();
            HttpContext context = HttpContext.Current;
            newData.Add(new InstanceData(InstanceDataType.Value, nameof(context.Request.UserAgent) , context.Request.UserAgent));
            return newData;

        }
    }
}