using MongoDB.Bson;
using NotX.Models.Article;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        public async Task<ActionResult> AddArticleDetail(ArticleListModel model)
        {
            await model.AddArticleDetail();

            return View("ArticleList");
        }
        public async Task<ActionResult> AddFavoriteNumber(ArticleDetailModel model)
        {
            await model.AddFavoriteNumber();

            return View("ArticleList");
        }
    }
}