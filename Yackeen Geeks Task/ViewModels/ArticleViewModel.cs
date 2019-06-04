using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yackeen_Geeks_Task.Models;

namespace Yackeen_Geeks_Task.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime PublishDate { get; set; }

        public string AuthorId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<Category> CategoriesList { get; set; }


        public ArticleViewModel()
        {

        }

        public ArticleViewModel(Article model)
        {
            Id = model.Id;
            Title = model.Title;
            Description = model.Description;
            ImageUrl = model.ImageUrl;
            PublishDate = model.PublishDate;
            CategoryId = model.CategoryId;
        }
    }
}