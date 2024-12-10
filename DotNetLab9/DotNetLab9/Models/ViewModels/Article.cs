namespace DotNetLab9.Models.ViewModels
{
    public enum FoodType { Diary, Meat, Fruit, Vegetable, Bakery}
    public class Article
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public FoodType Type { get; set; } 
       
        public Article()
        {

        }


        public Article(int id,string name, float price, DateTime expirationDate, FoodType type)
        {
            Id = Id;
            Name = name;
            Price = price;
            ExpirationDate = expirationDate;
            Type = type;
        }
    }

}
