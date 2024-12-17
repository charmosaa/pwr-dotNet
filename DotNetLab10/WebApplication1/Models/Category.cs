﻿namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Article> Articles { get; set; }

        public Category() { }
        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    
    }
}