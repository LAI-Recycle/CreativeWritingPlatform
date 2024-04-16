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

        /// <summary>
        /// 測試登入
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginTest()
        {
            //驗證成功後
            Session["UserRole"] = "Member";
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