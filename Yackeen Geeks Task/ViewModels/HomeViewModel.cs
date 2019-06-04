using System.Collections.Generic;
using Yackeen_Geeks_Task.Models;

namespace Yackeen_Geeks_Task.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Category> CategoriesList { get; set; }
        public IEnumerable<Article> ArticlesList { get; set; }

    }
}