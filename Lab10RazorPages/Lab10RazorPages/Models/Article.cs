
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Lab10RazorPages.Models;

namespace Lab10RazorPages.Models
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
            [Required]
            [Display(Name = "Category")]
            public int CategoryId { get; set; }


            public Category? Category { get; set; }
            [MaxLength(255)]
            [Display(Name = "Image")]
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


