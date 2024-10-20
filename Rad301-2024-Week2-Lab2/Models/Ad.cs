namespace Rad301_2024_Week2_Lab2.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }

        public int SellerId { get; set; }
        public int CategoryId { get; set; }

        public Seller Seller { get; set; }
        public Category Category { get; set; }
    }
}
