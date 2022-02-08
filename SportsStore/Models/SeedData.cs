using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SportsStore.Models
{
    public class SeedData
    {
        public static void EnsurePopulated (IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<StoreDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                    new Product
                    {
                        Name = "Kayak",
                        Description = "A boat for one person",
                        Category = "Watersports",
                        Price = 275
                    },
                    new Product 
                    { 
                        Name = "LifeJacket",
                        Description = "Protective and fash...",
                        Category = "Watersports", 
                        Price = 48.95m
                    },
                    new Product
                    {
                        Name = "Soccer Ball",
                        Description = "Fifa approved",
                        Category = "Soccer",
                        Price = 19.5m
                    },
                    new Product
                    {
                        Name = "Corner Flag",
                        Description = "Fashion Corner Flag!!!",
                        Category = "Soccer",
                        Price = 48.95m
                    },
                    new Product
                    {
                        Name = "Stadium",
                        Description = "35000 seat flat stadium",
                        Category = "Soccer",
                        Price = 79500
                    },
                    new Product
                    {
                        Name = "Thinking cap",
                        Description = "For improve brain",
                        Category = "Chess",
                        Price = 16
                    },
                    new Product
                    {
                        Name = "Unsteady chair",
                        Description = "Secretly give your opponent a disadvantage",
                        Category = "Chess",
                        Price = 29.95m
                    },
                    new Product
                    {
                        Name = "Human Chess-Board",
                        Description = "A fun game for a family",
                        Category = "Chess",
                        Price = 75
                    },
                    new Product
                    {
                        Name = "Blind-Blind King",
                        Description = "gold plated king",
                        Category = "Chess",
                        Price = 1200
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
