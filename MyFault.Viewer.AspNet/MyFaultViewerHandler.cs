using System.Diagnostics.CodeAnalysis;
using System.Web;
using MyFault.Viewer.AspNet.Actions;
using Newtonsoft.Json;

namespace MyFault.Viewer.AspNet
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class MyFaultViewerHandler : IHttpHandler
    {
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public bool IsReusable { get; }

        public void ProcessRequest(HttpContext context)
        {
            BuildResponse(context);
        }

        private void BuildResponse(HttpContext context)
        {
            string action = context.Request.QueryString["action"];
            object response = GetResponseObject(action, context);
            
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(response));
            context.Response.End();
        }

        private object GetResponseObject(string action, HttpContext context)
        {
            switch (action.ToLower())
            {
                case "fault":
                    return new FaultActionRenderer().RenderResponse(context);
                case "instance":
                    return new InstanceActionRenderer().RenderResponse(context);
                case "data":
                    return new DataActionRenderer().RenderResponse(context);
            }

            return null;
        }
    }
}