using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MebelWeb.DatabaseContext;
using MebelWeb.Models;

namespace MebelWeb.Controllers
{
    public class Zakupka_syriaController : Controller
    {
        private readonly AppDbContext _context;

        public Zakupka_syriaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Zakupka_syria
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Zakupka_syrias
                .Include(z => z.Syrio)
                .Include(z => z.Employee);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Zakupka_syria/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakupka_syria = await _context.Zakupka_syrias
                .Include(z => z.Syrio)
                .Include(z => z.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zakupka_syria == null)
            {
                return NotFound();
            }

            return View(zakupka_syria);
        }

        // GET: Zakupka_syria/Create
        public IActionResult Create()
        {
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO");
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala");
            return View();
        }

        // POST: Zakupka_syria/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,syrio,Count,Sum,data,employee")] Zakupka_syria zakupka_syria)
        {
            ModelState.Remove("syrio");
            ModelState.Remove("employee");

            if (_context.Budjets.First().budjet < zakupka_syria.Sum)
            {
                ModelState.AddModelError(string.Empty, "Не хватает денег.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(zakupka_syria);
                var existingBudjet = _context.Budjets.FirstOrDefault();
                existingBudjet.budjet-=zakupka_syria.Sum;
                _context.Update(existingBudjet);

                var syrios = _context.Syrios.Find(zakupka_syria.syrio);
                syrios.Sum += zakupka_syria.Sum;
                syrios.Count += zakupka_syria.Count;
                _context.Update(syrios);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", zakupka_syria.employee);
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala", zakupka_syria.syrio);
            return View(zakupka_syria);
        }

        // GET: Zakupka_syria/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakupka_syria = await _context.Zakupka_syrias.FindAsync(id);
            if (zakupka_syria == null)
            {
                return NotFound();
            }
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala", zakupka_syria.syrio);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", zakupka_syria.employee);
            return View(zakupka_syria);
        }

        // POST: Zakupka_syria/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,syrio,Count,Sum,data,employee")] Zakupka_syria zakupka_syria)
        {
            if (id != zakupka_syria.ID)
            {
                return NotFound();
            }

            ModelState.Remove("syrio");
            ModelState.Remove("employee");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zakupka_syria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Zakupka_syriaExists(zakupka_syria.ID))
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
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala", zakupka_syria.syrio);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", zakupka_syria.employee);
            return View(zakupka_syria);
        }

        // GET: Zakupka_syria/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakupka_syria = await _context.Zakupka_syrias
                .Include(z => z.Syrio)
                .Include(z => z.Employee)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (zakupka_syria == null)
            {
                return NotFound();
            }

            return View(zakupka_syria);
        }

        // POST: Zakupka_syria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zakupka_syria = await _context.Zakupka_syrias.FindAsync(id);
            if (zakupka_syria != null)
            {
                _context.Zakupka_syrias.Remove(zakupka_syria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Zakupka_syriaExists(int id)
        {
            return _context.Zakupka_syrias.Any(e => e.ID == id);
        }
    }
}
