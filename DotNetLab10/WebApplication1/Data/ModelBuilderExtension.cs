using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category(4,"Other"),
               new Category(1, "Food"),
               new Category(2, "Toys"),
               new Category(3, "Flowers")

               ); ;

            modelBuilder.Entity<Article>().HasData(
                new Article(1, "banana", 1.5m,1, "/upload/banan.jpg"),
                new Article(2, "cheese", 2.79m,1),
                new Article(3, "barbie", 11.5m, 2, "/upload/barbie.jpg"),
                new Article(4, "car", 12.79m, 2,"/upload/car.jpg"),
                new Article(5, "sunflower", 1.5m, 3, "/upload/sunflower.jpg")
                ); ;



        }
    }
}
