using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        /// 登入介面
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Remove("UserRole");

            return RedirectToAction("Index", "Home");
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
        /// 登入
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            //驗證成功後
            Session["UserRole"] = "Member";
            return RedirectToAction("Index", "Home");
        }
    }
}