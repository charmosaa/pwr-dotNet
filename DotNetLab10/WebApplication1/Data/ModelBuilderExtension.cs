using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
               new Category(1, "Food"),
               new Category(2, "Toys"),
               new Category(3, "Flowers")
               ); ;

            modelBuilder.Entity<Article>().HasData(
                new Article(1, "banana", 1.5m,1, "https://th-thumbnailer.cdn-si-edu.com/Yick6se-AqxGPo1w2caBgFSgFsY=/fit-in/1200x0/https://tf-cmsv2-smithsonianmag-media.s3.amazonaws.com/filer/20110721125011banana2.jpg"),
                new Article(2, "cheese", 2.79m,1),
                new Article(3, "barbie", 11.5m, 2),
                new Article(4, "car", 12.79m, 2),
                new Article(5, "sunflower", 1.5m, 3)
                ); ;



        }
    }
}
