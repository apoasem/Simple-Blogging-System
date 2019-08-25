using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Yackeen_Geeks_Task.Models;
using Yackeen_Geeks_Task.ViewModels;

namespace Yackeen_Geeks_Task.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                CategoriesList = db.Categories.ToList(),
                ArticlesList = db.Articles.Include(a => a.Author).Include(a => a.Category).ToList()
            };

            return View(viewModel);
        }

        public ActionResult Info()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}