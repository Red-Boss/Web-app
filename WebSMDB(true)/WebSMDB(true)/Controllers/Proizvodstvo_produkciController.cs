using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MebelWeb.DatabaseContext;
using MebelWeb.Models;

namespace MebelWeb.Controllers
{
    public class Proizvodstvo_produkciController : Controller
    {
        private readonly AppDbContext _context;

        public Proizvodstvo_produkciController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Proizvodstvo_produkci
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Proizvodstvo_produkcis
                .Include(p => p.Employee)
                .Include(p => p.Produkcia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Proizvodstvo_produkci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodstvo_produkci = await _context.Proizvodstvo_produkcis
                .Include(p => p.Employee)
                .Include(p => p.Produkcia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proizvodstvo_produkci == null)
            {
                return NotFound();
            }

            return View(proizvodstvo_produkci);
        }

        // GET: Proizvodstvo_produkci/Create
        public IActionResult Create()
        {
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO");
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie");
            return View();
        }

        // POST: Proizvodstvo_produkci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,produkcia,Count,data,employee")] Proizvodstvo_produkci proizvodstvo_produkci)
        {
            ModelState.Remove("employee");
            ModelState.Remove("produkcia");

            var ingredients = _context.Ingridients.Where(i => i.produkcia == proizvodstvo_produkci.produkcia).ToList();

            // Проверка наличия достаточного сырья
            foreach (var ingridient in ingredients)
            {
                var requiredSyrio = ingridient.Count * proizvodstvo_produkci.Count;

                if (requiredSyrio > _context.Syrios.Find(ingridient.syrio).Count)
                {
                    ModelState.AddModelError(string.Empty, "Не хватает сырья.");
                }
            }

            if (ModelState.IsValid)
            {
                decimal sum = 0;
                // Обновление данных о сырье
                foreach (var ingridient in ingredients)
                    {
                        var syrio = _context.Syrios.Find(ingridient.syrio);
                    var usedSyrio = (syrio.Sum / syrio.Count) * ingridient.Count * proizvodstvo_produkci.Count;
                    sum += usedSyrio;
                    syrio.Sum -= usedSyrio;
                    syrio.Count -= ingridient.Count * proizvodstvo_produkci.Count;
                    _context.Update(syrio);
                }
                var produkciaa = _context.Produkcias.Find(proizvodstvo_produkci.produkcia);
                produkciaa.Sum += sum;
                produkciaa.Count += proizvodstvo_produkci.Count;
                _context.Update(produkciaa);
                _context.Add(proizvodstvo_produkci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Заполнение ViewData
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", proizvodstvo_produkci.employee);
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", proizvodstvo_produkci.produkcia);
            return View(proizvodstvo_produkci);
        }


        // GET: Proizvodstvo_produkci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodstvo_produkci = await _context.Proizvodstvo_produkcis.FindAsync(id);
            if (proizvodstvo_produkci == null)
            {
                return NotFound();
            }
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", proizvodstvo_produkci.produkcia);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", proizvodstvo_produkci.employee);
            return View(proizvodstvo_produkci);
        }

        // POST: Proizvodstvo_produkci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,produkcia,Count,data,employee")] Proizvodstvo_produkci proizvodstvo_produkci)
        {
            if (id != proizvodstvo_produkci.ID)
            {
                return NotFound();
            }
            ModelState.Remove("employee");
            ModelState.Remove("produkcia");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proizvodstvo_produkci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Proizvodstvo_produkciExists(proizvodstvo_produkci.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", proizvodstvo_produkci.produkcia);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO" +
                "", proizvodstvo_produkci.employee);
            return View(proizvodstvo_produkci);
        }

        // GET: Proizvodstvo_produkci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proizvodstvo_produkci = await _context.Proizvodstvo_produkcis
                .Include(p => p.Employee)
                .Include(p => p.Produkcia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (proizvodstvo_produkci == null)
            {
                return NotFound();
            }

            return View(proizvodstvo_produkci);
        }

        // POST: Proizvodstvo_produkci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proizvodstvo_produkci = await _context.Proizvodstvo_produkcis.FindAsync(id);
            if (proizvodstvo_produkci != null)
            {
                _context.Proizvodstvo_produkcis.Remove(proizvodstvo_produkci);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Proizvodstvo_produkciExists(int id)
        {
            return _context.Proizvodstvo_produkcis.Any(e => e.ID == id);
        }
    }
}
