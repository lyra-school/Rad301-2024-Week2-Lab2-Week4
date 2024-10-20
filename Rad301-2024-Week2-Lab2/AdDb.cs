using Microsoft.EntityFrameworkCore;
using Rad301_2024_Week2_Lab2.Models;

namespace Rad301_2024_Week2_Lab2
{
    public class AdDb : DbContext
    {
        public AdDb(DbContextOptions<AdDb> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Ad> Ads => Set<Ad>();
        public DbSet<Seller> Sellers => Set<Seller>();
        public DbSet<Category> Categories => Set<Category>();
    }
}
