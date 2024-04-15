using NotX.Models.Article;
using NotX.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace NotX.Controllers.Category
{
    public class CategoryController : Controller
    {
        public async Task<ActionResult> CategoryList(CategoryListModel model)
        {

            return View(model);
        }
    }
}