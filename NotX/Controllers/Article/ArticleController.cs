using NotX.Filters;
using NotX.Models.Article;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotX.Controllers.Article
{
    [AuthorizeFilter(UserRole.Member)]
    public class ArticleController : Controller
    {
        public async Task<ActionResult> ArticleList(ArticleListModel model)
        {
            model.InitDict();
            await model.GetArticleList();

            return View(model);
        }

        public async Task<ActionResult> ArticleDetail(ArticleDetailModel model)
        {
            //下一頁?
            if (model.Choose_ArticleId <= 0)
            {
                model.Choose_ArticleId = 2;
            }

            await model.GetArticleDetail();
            await model.GetUserDetail();

            if (model.ActionType == "read")
            {
                await model.AddClickNumber();
            }

            return View(model);
        }

        public async Task<ActionResult> ArticleCreateDetail(ArticleCreateDetailModel model)
        {
            if (model.ActionType == "create")
            {
                model.InputArticle.AuthorID = Convert.ToInt32(Session["UserMemberID"]);
                model.InputArticle.Author = Session["UserName"].ToString();
                await model.AddArticleDetail();
            }
            else if (model.ActionType == "read")
            {
                model.GetAddArticleDetail();
                return View(model);
            }
            else if (model.ActionType == "back")
            {
                
            }
            return RedirectToAction("ArticleList", "Article");
        }

        public async Task<ActionResult> AddFavoriteNumber(ArticleDetailModel model)
        {
            await model.AddFavoriteNumber();    

            return Json(true);
        }
    }
}