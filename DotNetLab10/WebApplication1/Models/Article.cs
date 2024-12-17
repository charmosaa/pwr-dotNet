using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.Models
{
     public class Article
    {
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string? Name { get; set; }
        [Range(0, 99999.99)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        [MaxLength(255)]
        public string? ImagePath { get; set; }
        public Article()
        {

        }


        public Article(int id, string name, decimal price, int categoryId, string? imagePath = null)
        {
            Id = id;
            Name = name;
            Price = price;
            CategoryId = categoryId;
            ImagePath = imagePath;
        }
    }

}
