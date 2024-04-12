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
            await model.GetArticleDetail();

            return View(model);
        }

        public async Task<ActionResult> AddArticleDetail(ArticleListModel model)
        {
            await model.AddArticleDetail();

            return View("ArticleList");
        }
    }
}