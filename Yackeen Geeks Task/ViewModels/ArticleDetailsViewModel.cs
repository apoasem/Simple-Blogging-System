using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yackeen_Geeks_Task.Models;

namespace Yackeen_Geeks_Task.ViewModels
{
    public class ArticleDetailsViewModel
    {
        public Article Article { get; set; }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string Content { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string VisitorName { get; set; }

        [Required]
        public int ArticleId { get; set; }

        public IEnumerable<Comment> CommentsList { get; set; }
    }
}