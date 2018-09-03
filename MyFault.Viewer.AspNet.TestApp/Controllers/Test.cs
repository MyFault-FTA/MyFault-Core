using System.Net.Http;
using System.Web.Mvc;

namespace MyFault.Viewer.AspNet.TestApp.Controllers
{
    
    public class Test : Controller
    {
        // GET
        [HttpGet]
        public ActionResult Index()
        {
            return View();

        }
    }
}