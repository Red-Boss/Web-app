using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MebelWeb.Seeds
{
    public class SeedData5
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context == null || context.Proizvodstvo_produkcis == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Proizvodstvo_produkcis.Any())
                {
                    return;   // DB has been seeded
                }


                context.Zakupka_syrias.AddRange(
                    new Zakupka_syria
                    {
                        syrio = 1,
                        Count = 5,
                        Sum = 1000,
                        employee = 1,
                        data = DateTime.Now
                    },
                    new Zakupka_syria
                    {
                        syrio = 2,
                        Count = 5,
                        Sum = 1000,
                        employee = 2,
                        data = DateTime.Now
                    },
                    new Zakupka_syria
                    {
                        syrio = 3,
                        Count = 5,
                        Sum = 1000,
                        employee = 1,
                        data = DateTime.Now
                    }

                ) ;
                context.SaveChanges();

                context.Proizvodstvo_produkcis.AddRange(
                    new Proizvodstvo_produkci
                    {
                        produkcia = 1,
                        Count = 5,
                        employee = 1,
                        data = DateTime.Now
                    },
                    new Proizvodstvo_produkci
                    {
                        produkcia = 2,
                        Count = 5,
                        employee = 1,
                        data = DateTime.Now
                    }

                );
                context.SaveChanges();
            }
        }
    
    }
}
