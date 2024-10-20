using Microsoft.EntityFrameworkCore;

namespace Rad301_2024_Week2_Lab2.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var context = new AdDb(
                serviceProvider.GetRequiredService<DbContextOptions<AdDb>>()))
            {
                if(context == null || context.Ads == null)
                {
                    throw new ArgumentNullException("Null Context");
                }

                if(!context.Sellers.Any() && !context.Categories.Any())
                {
                    Category cat1 = new Category
                    {
                        Description = "Medical"
                    };

                    Category cat2 = new Category
                    {
                        Description = "Gaming"
                    };
                    
                    context.Categories.Add(cat1);
                    context.Categories.Add(cat2);

                    Seller sell1 = new Seller
                    {
                        Name = "Enamel Frontiers"
                    };

                    Seller sell2 = new Seller
                    {
                        Name = "Generic Inc."
                    };

                    context.Sellers.Add(sell1);
                    context.Sellers.Add(sell2);

                    context.Ads.AddRange(
                        new Ad
                        {
                            Description = "9 out of 10 dentists recommend Hotgate!",
                            Price = 10.0f,
                            Category = cat1,
                            Seller = sell1
                        },
                        new Ad
                        {
                            Description = "Raid Match-3 Heroes",
                            Price = 42.0f,
                            Category = cat2,
                            Seller = sell2
                        }
                    );
                }

                context.SaveChanges();
            }
        }
    }
}
