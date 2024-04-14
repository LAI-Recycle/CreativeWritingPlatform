using NotX.Models.Article;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NotX.Controllers.Article
{
    public class ArticleController : Controller
    {
        public async Task<ActionResult> ArticleList(ArticleListModel model)
        {
            await model.GetArticleList();

            return View(model);
        }

        public async Task<ActionResult> ArticleDetail(ArticleDetailModel model)
        {
            //暫時
            if (model.Choose_ArticleId <= 0)
            {
                model.Choose_ArticleId = 2;
            }

            await model.GetArticleDetail();

            return View(model);
        }

        public async Task<ActionResult> ArticleCreateDetail(ArticleCreateDetailModel model)
        {
            if (model.ActionType == "create")
            {
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