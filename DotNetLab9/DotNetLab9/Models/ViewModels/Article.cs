namespace DotNetLab9.Models.ViewModels
{
    public enum FoodType { Dairy, Meat, Fish, Fruit, Vegetable, Bakery}
    public class Article
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public FoodType Type { get; set; } 
       
        public Article()
        {

        }


        public Article(int id, string name, decimal price, DateTime expirationDate, FoodType type)
        {
            Id = id;
            Name = name;
            Price = price;
            ExpirationDate = expirationDate;
            Type = type;
        }
    }

}
