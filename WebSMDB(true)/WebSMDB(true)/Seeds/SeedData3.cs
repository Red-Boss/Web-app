using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MebelWeb.Seeds
{
    public class SeedData3
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context == null || context.Produkcias == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Produkcias.Any())
                {
                    return;   // DB has been seeded
                }


                context.Syrios.AddRange(
                    new Syrio
                    {
                        Naimenovanie_materiala="Картон",
                        edinica_izmerenia=1,
                        Count=4,
                        Sum=1000
                    },
                    new Syrio
                    {
                        Naimenovanie_materiala = "Клей",
                        edinica_izmerenia = 2,
                        Count = 500,
                        Sum = 200
                    },
                    new Syrio
                    {
                        Naimenovanie_materiala = "Лента",
                        edinica_izmerenia = 1,
                        Count = 1,
                        Sum = 500
                    }

                );
                context.SaveChanges();
                context.Produkcias.AddRange(
                    new Produkcia
                    {
                        Naimenovanie="Упаковка",
                        edinica_izmerenia=1,
                        Count=4,
                        Sum=10000
                    },
                    new Produkcia
                    {
                        Naimenovanie = "Подарочная упаковка",
                        edinica_izmerenia = 2,
                        Count = 3,
                        Sum = 100
                    }

                );
                context.SaveChanges();
            }
        }
    
    }
}
