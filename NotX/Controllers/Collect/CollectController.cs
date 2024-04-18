using NotX.Models.User;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotX.Controllers.User
{
    public class CollectController : Controller
    {
        public async Task<ActionResult> CollectList(CollectListModel model)
        {
            if (model.ActionType == "read")
            {
                //model.Choose_CollectMemberID = Convert.ToInt32(Session["UserMemberID"]);
                //await model.GetCollectList();
            }
            else if (model.ActionType == "add")
            {
                model.Choose_CollectMemberID = Convert.ToInt32(Session["UserMemberID"]);
                await model.AddCollectList();
            }
            else if (model.ActionType == "delete")
            {
                await model.UpdateCollectList();
            }

            return Json(true);
        }
    }
}