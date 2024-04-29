using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MebelWeb.DatabaseContext;
using MebelWeb.Models;
using System.Globalization;

namespace MebelWeb.Controllers
{
    public class ZarplatasController : Controller
    {
        private readonly AppDbContext _context;

        public ZarplatasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Zarplatas
        public async Task<IActionResult> Index(int? year, int? month)
        {
            // Проверяем, если год и месяц не заданы, используем текущее время
            if (!year.HasValue || !month.HasValue)
            {
                var now = DateTime.Now;
                year ??= now.Year; // Если year пустой, присваиваем текущий год
                month ??= now.Month; // Если month пустой, присваиваем текущий месяц
            }

            // Запрос на получение зарплат с учетом заданного года и месяца
            var appDbContext = _context.Zarplatas.Include(z => z.Employee);
            var appDbContext2 = appDbContext.Where(i => i.Year == year && i.Month == month);
            if (!year.HasValue || !month.HasValue)
            {
                // Если год и месяц не указаны, используем текущую дату
                var now = DateTime.Now;
                year ??= now.Year;
                month ??= now.Month;
            }

            // Проверяем, есть ли уже зарплата для выбранного года и месяца
            var existingSalaries = _context.Zarplatas.Any(z => z.Year == year && z.Month == month);

            // Если записи о зарплате уже существуют, перенаправляем на страницу индекса
            if (!existingSalaries)
            {

            // список всех сотрудников
            var employees = _context.Employees.ToList();
            //    zarplata.ForProduction = _context.Proizvodstvo_produkcis.Where(i => i.employee == zarplata.employee && i.check == false).Count();
            //    zarplata.ForSale = _context.Prodaja_produkcis.Where(i => i.employee == zarplata.employee && i.check == false).Count();

            //    var common = zarplata.ForSale + zarplata.ForPurchase + zarplata.ForProduction;
            //    var oklad1 = _context.Employees.First().Oklad;
            //    var bonus = ((common * _context.Budjets.First().bonus) * oklad1) / 100;

            //    zarplata.Year = year;
            //    zarplata.Month = month;
            //    zarplata.Common = common;
            //    zarplata.oklad = oklad1;
            //    zarplata.Bonus = bonus;
            //    zarplata.General = bonus + oklad1;
            //    zarplata.given = false;
            //    ViewBag.given=zarplata.given==false?"Нет":"Да";
            // записи о зарплате для каждого сотрудника
            foreach (var employee in employees)
            {
                // новый объект зарплаты
                var zarplata = new Zarplata
                {
                    ForProduction = _context.Proizvodstvo_produkcis.Count(i => i.employee == employee.ID && !i.check),
                    ForSale = _context.Prodaja_produkcis.Count(i => i.employee == employee.ID && !i.check),
                    employee = employee.ID,
                    Year = year.Value,
                    Month = month.Value,
                };
                // добавить параметры
                var common = zarplata.ForSale + zarplata.ForPurchase + zarplata.ForProduction;
                var oklad1 = _context.Employees.First().Oklad;
                var budjetBonus = _context.Budjets.First().bonus;
                var bonus = ((common * budjetBonus) * oklad1) / 100;

                zarplata.Common = common;
                zarplata.oklad = oklad1;
                zarplata.Bonus = bonus;
                zarplata.General = bonus + oklad1;
                zarplata.given = false;
                ViewBag.given = zarplata.given == false ? "Нет" : "Да";

                // Добавляем новую запись о зарплате в контекст базы данных
                _context.Zarplatas.Add(zarplata);
            }
            }
            // Сохраняем все изменения в базе данных
            _context.SaveChanges();
            /*// Создаем SelectList с названиями месяцев
            var years = Enumerable.Range(DateTime.Now.Year - 9, 10).Select(year => new SelectListItem { Text = year.ToString(), Value = year.ToString() });
            ViewData["year"] = new SelectList(years, "Value", "Text", year);
*/
            ViewBag.year = year;
            ViewBag.monthI = month;

            // Создаем SelectList с названиями месяцев
            var months = Enumerable.Range(1, 12)
                                   .Select(m => new { Value = m, Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m) });
            ViewData["month"] = new SelectList(months, "Value", "Text", month);

            decimal s = 0;
            if(appDbContext2!=null)foreach(var item in appDbContext2.Where(i=>i.given==false))
                {
                    s += item.General;
                }
            ViewBag.sum =s;

            return View(await appDbContext2.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int year, int month,decimal sum)
        {

            var budj = _context.Budjets.First();
            // Запрос на получение зарплат с учетом заданного года и месяца
            var appDbContext = _context.Zarplatas.Include(z => z.Employee);
            var appDbContext2 = appDbContext.Where(i => i.Year == year && i.Month == month);
            /*decimal s = 0;
            if (appDbContext2 != null) foreach (var item in appDbContext2)
                {
                    s += item.General;
                }*/

            if (sum > budj.budjet)
            {
                ModelState.AddModelError(string.Empty, "Не хватает бюджета.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                        budj.budjet -= sum;
                        _context.Update(budj);
                    foreach(var item in appDbContext2.Where(i => i.given == false))
                    {
                        item.given = true;
                        _context.Update(item);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
                return RedirectToAction(nameof(Index), new { year = year, month = month });
            }
            ViewBag.year = year;

            // Создаем SelectList с названиями месяцев
            var months = Enumerable.Range(1, 12)
                                   .Select(m => new { Value = m, Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m) });
            ViewData["month"] = new SelectList(months, "Value", "Text", month);
            return View(await appDbContext2.ToListAsync());
        }

        // GET: Zarplatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarplata = await _context.Zarplatas
                .Include(z => z.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zarplata == null)
            {
                return NotFound();
            }

            return View(zarplata);
        }

        // GET: Zarplatas/Create
        public IActionResult Create(int? employeeId, int year, int month)
        {
            if (employeeId == null) employeeId = _context.Employees.First().ID;

            Zarplata zarplata = new Zarplata();
            zarplata.employee = employeeId;
            zarplata.ForPurchase = _context.Zakupka_syrias.Where(i => i.employee == zarplata.employee && i.check == false).Count();
            zarplata.ForProduction = _context.Proizvodstvo_produkcis.Where(i => i.employee == zarplata.employee && i.check == false).Count();
            zarplata.ForSale = _context.Prodaja_produkcis.Where(i => i.employee == zarplata.employee && i.check == false).Count();

            var common = zarplata.ForSale + zarplata.ForPurchase + zarplata.ForProduction;
            var oklad1 = _context.Employees.First().Oklad;
            var bonus = ((common * _context.Budjets.First().bonus) * oklad1) / 100;

            zarplata.Year = year;
            zarplata.Month = month;
            zarplata.Common = common;
            zarplata.oklad = oklad1;
            zarplata.Bonus = bonus;
            zarplata.General = bonus + oklad1;
            zarplata.given = false;
            ViewBag.given = zarplata.given == false ? "Нет" : "Да";
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", employeeId);
            return View(zarplata);
        }



        // POST: Zarplatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int year, int month,[Bind("Id,employee,Year,Month,ForPurchase,ForProduction,ForSale,Common,oklad,Bonus,General,given")] Zarplata zarplata)
        {
            
            var existingZarplatas = await _context.Zarplatas
                .FirstOrDefaultAsync(i => i.Year == zarplata.Year && i.Month == zarplata.Month && i.employee==zarplata.employee);

            if (existingZarplatas != null)
            {
                ModelState.AddModelError(string.Empty, "Зарплату можно выдать только раз в месяц.");
            }
            var budj = _context.Budjets.First();

            if(zarplata.given && zarplata.General > budj.budjet)
            {
                ModelState.AddModelError(string.Empty, "Не хватает бюджета.");
            }
            var pursh = _context.Zakupka_syrias.Where(i => i.employee == zarplata.employee && i.check == false).ToList();
            var prods = _context.Proizvodstvo_produkcis.Where(i => i.employee == zarplata.employee && i.check == false).ToList();
            var sales = _context.Prodaja_produkcis.Where(i => i.employee == zarplata.employee && i.check == false).ToList();
           
            if (ModelState.IsValid)
            {
                foreach(var temp in pursh){
                    temp.check = true;
                    _context.Update(temp);
                }
                foreach (var temp in prods){
                    temp.check = true;
                    _context.Update(temp);
                }
                foreach (var temp in sales){
                    temp.check = true;
                    _context.Update(temp);
                }
                if (zarplata.given)
                {
                    budj.budjet -= zarplata.General;
                    _context.Update(budj);
                }
                _context.Add(zarplata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new {year=zarplata.Year,month=zarplata.Month});
            }
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", zarplata.employee);
            return View(zarplata);
        }

        // GET: Zarplatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var zarplata = await _context.Zarplatas.FindAsync(id);
            if (zarplata == null)
            {
                return NotFound();
            }
            ViewBag.empl= _context.Employees.Find(zarplata.employee).FIO;
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", zarplata.employee);
            return View(zarplata);
        }

        // POST: Zarplatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,employee,Year,Month,ForPurchase,ForProduction,ForSale,Common,oklad,Bonus,General,given")] Zarplata zarplata)
        {
            if (id != zarplata.Id)
            {
                return NotFound();
            }

            var budj = _context.Budjets.First();

            if (zarplata.given && zarplata.General > budj.budjet)
            {
                ModelState.AddModelError(string.Empty, "Не хватает бюджета.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (zarplata.given)
                    {
                        budj.budjet -= zarplata.General;
                        _context.Update(budj);
                    }
                    _context.Update(zarplata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZarplataExists(zarplata.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { year = zarplata.Year, month = zarplata.Month });
            }
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", zarplata.employee);
            return View(zarplata);
        }

        // GET: Zarplatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zarplata = await _context.Zarplatas
                .Include(z => z.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (zarplata == null)
            {
                return NotFound();
            }

            return View(zarplata);
        }

        // POST: Zarplatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zarplata = await _context.Zarplatas.FindAsync(id);
            if (zarplata != null)
            {
                _context.Zarplatas.Remove(zarplata);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZarplataExists(int id)
        {
            return _context.Zarplatas.Any(e => e.Id == id);
        }
    }
}
