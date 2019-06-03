using System;
using System.ComponentModel.DataAnnotations;

namespace Yackeen_Geeks_Task.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}