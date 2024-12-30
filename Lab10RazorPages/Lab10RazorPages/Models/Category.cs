using System.ComponentModel.DataAnnotations;

namespace Lab10RazorPages.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public Category() { }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
