using System.Web;

namespace MyFault.Viewer.AspNet.Actions
{
    public interface IActionRenderer
    {
        object RenderResponse(HttpContext context);
    }
}