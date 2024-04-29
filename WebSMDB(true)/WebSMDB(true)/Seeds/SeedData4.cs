using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MebelWeb.Seeds
{
    public class SeedData4
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context == null || context.Ingridients == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Ingridients.Any())
                {
                    return;   // DB has been seeded
                }


                context.Ingridients.AddRange(
                    new Ingridient
                    {
                        produkcia=1,
                        syrio=1,
                        Count=1
                    },
                    new Ingridient
                    {
                        produkcia = 1,
                        syrio = 2,
                        Count = 1
                    },
                    new Ingridient
                    {
                        produkcia = 2,
                        syrio = 1,
                        Count = 2
                    },
                    new Ingridient
                    {
                        produkcia = 2,
                        syrio = 3,
                        Count = 4
                    }

                );
                context.SaveChanges();
            }
        }
    
    }
}
