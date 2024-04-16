using NotX.Models.Member;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotX.Controllers.Login
{
    public class MemberController: Controller
    {
        /// <summary>
        /// 登入介面
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginPage()
        {
            return View();
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Login(LoginModel model)
        {
            bool LoginResponse = await model.CheckMemberDetail();

            if (LoginResponse)
            {
                Session["UserRole"] = "Member";
                return RedirectToAction("Index", "Home");
            }
            else 
            {
                //登入失敗
                ViewBag.Message = "登入錯誤";
                return View("LoginPage");
            }
        }

        /// <summary>
        /// 註冊介面
        /// </summary>
        /// <returns></returns>
        public ActionResult SignUpPage()
        {
            return View();
        }

        /// <summary>
        /// 註冊
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
            bool MemberExist = await model.CheckMemberExist();

            if (MemberExist)
            {
                //你已經存在
                return RedirectToAction("SignUpPage", "Member");
            }
            else
            {
                //註冊成功
                await model.AddMemberDetail();
                return RedirectToAction("LoginPage", "Member");
            }
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Remove("UserRole");

            return RedirectToAction("Index", "Home");
        }
    }
}