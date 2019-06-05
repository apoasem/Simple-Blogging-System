using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yackeen_Geeks_Task.Models;
using Yackeen_Geeks_Task.ViewModels;

namespace Yackeen_Geeks_Task.Controllers
{
    [Authorize(Roles = "Admins")]
    public class ArticlesController : Controller
    {
        private readonly ApplicationDbContext db;

        public ArticlesController()
        {
            db = new ApplicationDbContext();

        }
        // GET: Articles
        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();

            var articles = db.Articles.Include(a => a.Category).Include(a => a.Author).Where(a => a.AuthorId == currentUserId).AsQueryable();

            return View(articles.ToList());
        }

        [AllowAnonymous]
        // GET: Articles/Details/5
        public ActionResult Details(int id)
        {
            var article = db.Articles.Include(a => a.Author).Include(a => a.Category).SingleOrDefault(a => a.Id == id);

            var comments = db.Comments.Where(c => c.ArticleId == article.Id).ToList();

            if (article == null)
            {
                return HttpNotFound();
            }

            var ViewModel = new ArticleDetailsViewModel
            {
                Article = article,
                ArticleId = article.Id,
                CommentsList = comments
            };

            return View(ViewModel);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            var viewModel = new ArticleViewModel
            {
                CategoriesList = db.Categories.ToList()
            };

            return View(viewModel);
        }

        // POST: Articles/Create
        [HttpPost]
        public ActionResult Create(Article article, HttpPostedFileBase imageUrl)
        {
            var currentUserId = User.Identity.GetUserId();

            article.PublishDate = DateTime.Now;
            article.AuthorId = currentUserId;

            if (imageUrl != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(imageUrl.FileName) + DateTime.Now.ToLongDateString() + Path.GetExtension(imageUrl.FileName);

                string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                imageUrl.SaveAs(path);

                article.ImageUrl = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return PartialView("_ArticlesTablePartial", db.Articles.Where(a => a.AuthorId == currentUserId).ToList());
            }

            var viewModel = new ArticleViewModel
            {
                CategoriesList = db.Categories.ToList()
            };

            return View(viewModel);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            var currentUserId = User.Identity.GetUserId();

            var viewModel = new ArticleViewModel(article)
            {
                CategoriesList = db.Categories.ToList(),
                AuthorId = currentUserId
            };

            return View(viewModel);
        }

        // POST: Articles/Edit/5
        [HttpPost]
        public ActionResult Edit(Article article, HttpPostedFileBase imageUrl)
        {
            var currentUserId = User.Identity.GetUserId();

            if (imageUrl != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(imageUrl.FileName) + DateTime.Now.ToLongDateString() + Path.GetExtension(imageUrl.FileName);

                string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);

                imageUrl.SaveAs(path);

                article.ImageUrl = fileName;
            }

            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("_ArticlesTablePartial", db.Articles.Where(a => a.AuthorId == currentUserId).ToList());
            }

            return View(article);
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Article article = db.Articles.Find(id);

            if (article == null)
            {
                return HttpNotFound();
            }

            db.Articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [AllowAnonymous]
        public PartialViewResult FilterByCategory(int id)
        {
            var articles = db.Articles.Include(a => a.Author).Include(a => a.Category).AsQueryable();

            var numOfarticles = articles.Count();

            var articlesList = new List<Article>();

            if (numOfarticles == 0)
            {
                return PartialView("_ArticlesPartial", articlesList);
            }

            articlesList = articles.Where(a => a.CategoryId == id).ToList();

            return PartialView("_ArticlesPartial", articlesList);
        }
    }
}
