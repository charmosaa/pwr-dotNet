namespace WebApplication1.Models
{
    public class OrderModel
    {
        public List<(Article Article, int Quantity)> CartItems { get; set; } = new List<(Article Article, int Quantity)>();
        public decimal TotalCost { get; set; }
    }
}
