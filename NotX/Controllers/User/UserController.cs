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
        public async Task<ActionResult> UserPageList(UserPageListModel model)
        {
            model.InputMemberID = Convert.ToInt32(Session["UserMemberID"]);
            await model.GetUserDetail();
            await model.GetArticleList();
            await model.GetUserCollectList();
            return View(model);
        }

        /// <summary>
        /// 更新個人介面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ActionResult> UpdateUserPage(EditUserPageModel model)
        {
            model.InputMemberID = Convert.ToInt32(Session["UserMemberID"]);
            await model.UpdateUserDetail();

            return RedirectToAction("UserPageList", "User");
        }
    }
}