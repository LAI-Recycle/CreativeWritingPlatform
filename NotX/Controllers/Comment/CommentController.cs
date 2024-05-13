using NotX.Models.Comment;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotX.Controllers.Comment
{
    public class CommentController: Controller
    {
        public async Task<ActionResult> CommentDetail(CommentModel model)
        {
            if (model.ActionType == "read")
            {

            }
            else if (model.ActionType == "addFavorite")
            {
                await model.AddFavoriteNumber();
            }
            else if (model.ActionType == "addComment")
            {
                //20240513問題
                model.InputComment.MemberID = Convert.ToInt32(Session["UserMemberID"]);
                model.InputComment.MemberName = Session["UserName"].ToString();
                await model.AddCommentList();

            }
            else if (model.ActionType == "delete")
            {

            }

            return Json(true);
        }
    }
}