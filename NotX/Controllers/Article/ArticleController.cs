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

        public async Task<ActionResult> ArticleReadDetail(ArticleReadDetailModel model)
        {
            //上小於0 下一頁大於最大?修改?
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

        public async Task<ActionResult> ArticleUpdateDetail(ArticleUpdateDetailModel model)
        {
            
            if (model.ActionType == "create")
            {
                model.InputArticle.AuthorID = Convert.ToInt32(Session["UserMemberID"]);
                model.InputArticle.Author = Session["UserName"].ToString();
                await model.AddArticleDetail();
                var Choose_ArticleId = model.New_ArticleId;

                return RedirectToAction("ArticleReadDetail", "Article", new { Choose_ArticleId = Choose_ArticleId }) ;
            }
            else if (model.ActionType == "update")
            {
                await model.UpdateArticleDetail();
                var Choose_ArticleId = model.Choose_ArticleId;

                return RedirectToAction("ArticleReadDetail", "Article", new { Choose_ArticleId = Choose_ArticleId });
            }
            else if (model.ActionType == "edit")
            {
                await model.GetArticleDetail();
                return View(model);
            }
            else if (model.ActionType == "read")
            {
                return View();
            }
            else if (model.ActionType == "back")
            {
            }
            return RedirectToAction("ArticleList", "Article");
        }

        public async Task<ActionResult> AddFavoriteNumber(ArticleReadDetailModel model)
        {
            await model.AddFavoriteNumber();

            return Json(true);
        }
    }
}