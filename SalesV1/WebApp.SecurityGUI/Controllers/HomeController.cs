using System.Web.Mvc;

namespace WebApp.SecurityGUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return RedirectToAction("Index", "Categories"); 
        }

        public ActionResult Contact()
        {
            return RedirectToAction("Index", "Products"); 
        }
    }
}
