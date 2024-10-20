namespace Rad301_2024_Week2_Lab2.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<Ad> Ads { get; set; }
    }
}
