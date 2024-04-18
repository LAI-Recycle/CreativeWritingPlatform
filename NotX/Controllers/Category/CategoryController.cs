using NotX.Models.Category;
using System.Threading.Tasks;
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