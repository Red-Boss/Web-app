using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MebelWeb.Seeds;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new AppDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<AppDbContext>>()))
        {
            if (context == null || context.Budjets == null)
            {
                throw new ArgumentNullException("Null RazorPagesMovieContext");
            }

            // Look for any movies.
            if (context.Budjets.Any())
            {
                return;   // DB has been seeded
            }

            context.Budjets.AddRange(
                new Budjet
                {
                    budjet = 2500,
                    bonus = 5,
                    pr_prodaji = 30
                }

            );
            context.Doljnosts.AddRange(
                new Doljnost
                {
                    name_doljnost = "Директор"
                },
                new Doljnost
                {
                    name_doljnost = "Менеджер"
                }

            );
           
            context.SaveChanges();
        }
    }
}
