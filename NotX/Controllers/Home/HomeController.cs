using NotX.Filters;
using NotX.Models.Home;
using System.Web.Mvc;

namespace NotX.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HomeModel model)
        {
            if (model.ErrorMsg == "unauthorized")
            {
                ViewBag.Message = "請登入會員";
            }
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