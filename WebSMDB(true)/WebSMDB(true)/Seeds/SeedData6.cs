using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MebelWeb.Seeds
{
    public class SeedData6
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context == null || context.Prodaja_produkcis == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Prodaja_produkcis.Any())
                {
                    return;   // DB has been seeded
                }


                context.Prodaja_produkcis.AddRange(
                    new Prodaja_produkci
                    {
                        produkcia = 1,
                        Count = 5,
                        Sum = 1500,
                        employee = 1,
                        data = DateTime.Now
                    },
                    new Prodaja_produkci
                    {
                        produkcia = 2,
                        Count = 5,
                        Sum = 2000,
                        employee = 2,
                        data = DateTime.Now
                    }

                ) ;
                context.SaveChanges();

            }
        }
    
    }
}
