using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace MebelWeb.Seeds
{
    public class SeedData2
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AppDbContext>>()))
            {
                if (context == null || context.Employees == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.Employees.Any())
                {
                    return;   // DB has been seeded
                }

                
                context.Edinica_izmerenias.AddRange(
                    new Edinica_izmerenia
                    {
                        Naimenovanie = "Метр"
                    },
                    new Edinica_izmerenia
                    {
                        Naimenovanie = "Миллилитр"
                    },
                    new Edinica_izmerenia
                    {
                        Naimenovanie = "Сантиметр"
                    }

                );
                context.SaveChanges();
                context.Employees.AddRange(
                    new Employee
                    {
                        FIO="Именов Алишер",
                        Adres="Рабочий городок",
                        doljnost=1,
                        Number="0555829363",
                        Oklad=100000
                    },
                    new Employee
                    {
                        FIO = "Полукаров Ярослав",
                        Adres = "Кант",
                        doljnost = 2,
                        Number = "0555855363",
                        Oklad = 10000
                    }

                );

                context.SaveChanges();
                
            }
        }
        }
}
