using System;
using System.Linq;
using System.Web.Mvc;
using Yackeen_Geeks_Task.Models;
using Yackeen_Geeks_Task.ViewModels;

namespace Yackeen_Geeks_Task.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        public CommentsController()
        {
            db = new ApplicationDbContext();
        }

        // POST: Comments/Create
        [HttpPost]
        public ActionResult Create(Comment comment)
        {
            comment.Date = DateTime.Now;

            var commentList = db.Comments.Where(c => c.ArticleId == comment.ArticleId).ToList();

            var viewModel = new ArticleDetailsViewModel
            {
                CommentsList = commentList
            };

            if (!ModelState.IsValid)
            {
                return PartialView("_ArticleComments", viewModel);
            }

            db.Comments.Add(comment);
            db.SaveChanges();

            var updatedCommentList = db.Comments.Where(c => c.ArticleId == comment.ArticleId).ToList();

            viewModel = new ArticleDetailsViewModel
            {
                CommentsList = updatedCommentList
            };

            return PartialView("_ArticleComments", viewModel);
        }

        // POST: Comments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var comment = db.Comments.SingleOrDefault(c => c.Id == id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            db.Comments.Remove(comment);
            db.SaveChanges();

            return RedirectToAction("Details", "Articles", new { Id = comment.ArticleId });
        }
    }
}
