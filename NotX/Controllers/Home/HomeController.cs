using NotX.Filters;
using System.Web.Mvc;

namespace NotX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeFilter(UserRole.Member)]
        public ActionResult MemberPage() 
        {
            return View();
        }

        [AuthorizeFilter(UserRole.Admin)]
        public ActionResult AdminPage()
        {
            return View();
        }
    }
}