using NotX.Models.User;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotX.Controllers.User
{
    public class UserController: Controller
    {
        /// <summary>
        /// 個人介面
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserPage(UserPageModel model)
        {
            model.InputMemberID = Convert.ToInt32(Session["UserMemberID"]);
            await model.GetUserDetail();
            await model.GetArticleList();

            return View(model);
        }

        /// <summary>
        /// 修改個人資訊
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> EditUserPage(EditUserPageModel model)
        {
            model.InputMemberID = Convert.ToInt32(Session["UserMemberID"]);
            await model.GetUserDetail();

            return View(model);
        }

        public async Task<ActionResult> UpdateUserPage(EditUserPageModel model)
        {
            model.InputMemberID = Convert.ToInt32(Session["UserMemberID"]);
            await model.UpdateUserDetail();

            return RedirectToAction("UserPage", "User");
        }
    }
}