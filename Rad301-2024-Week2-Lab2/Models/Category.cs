namespace Rad301_2024_Week2_Lab2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Description { get; set; }

        public ICollection<Ad> Ads { get; set; }
    }
}
