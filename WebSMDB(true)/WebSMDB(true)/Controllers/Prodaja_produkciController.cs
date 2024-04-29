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
    public class Prodaja_produkciController : Controller
    {
        private readonly AppDbContext _context;

        public Prodaja_produkciController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Prodaja_produkci
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Prodaja_produkcis
                .Include(p => p.Employee)
                .Include(p => p.Produkcia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Prodaja_produkci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodaja_produkci = await _context.Prodaja_produkcis
                .Include(p => p.Employee)
                .Include(p => p.Produkcia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (prodaja_produkci == null)
            {
                return NotFound();
            }

            return View(prodaja_produkci);
        }

        // GET: Prodaja_produkci/Create
        public IActionResult Create()
        {
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie");
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO");
            return View();
        }

        // POST: Prodaja_produkci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,produkcia,Count,data,employee")] Prodaja_produkci prodaja_produkci)
        {
            ModelState.Remove("employee");
            ModelState.Remove("produkcia");

            var budj = _context.Budjets.First();

            var produkcias = _context.Produkcias.Find(prodaja_produkci.produkcia);
            

            if (prodaja_produkci.Count > produkcias.Count)
            {
                ModelState.AddModelError(string.Empty, "Не хватает продукции.");
            }

            if (ModelState.IsValid)
            {
                var avgsum = produkcias.Sum / produkcias.Count;
                var sebestoimost = avgsum * prodaja_produkci.Count;
                var sum = sebestoimost * ((decimal)(budj.pr_prodaji) / 100 + 1);
                produkcias.Sum -= sebestoimost;
                produkcias.Count -= prodaja_produkci.Count;
                prodaja_produkci.Sum = sum;
                budj.budjet += sum;
                _context.Update(produkcias);
                _context.Update(budj);
                _context.Add(prodaja_produkci);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", prodaja_produkci.produkcia);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", prodaja_produkci.employee);
            return View(prodaja_produkci);
        }

        // GET: Prodaja_produkci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodaja_produkci = await _context.Prodaja_produkcis.FindAsync(id);
            if (prodaja_produkci == null)
            {
                return NotFound();
            }
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", prodaja_produkci.produkcia);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", prodaja_produkci.employee);
            return View(prodaja_produkci);
        }

        // POST: Prodaja_produkci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,produkcia,Count,Sum,data,employee")] Prodaja_produkci prodaja_produkci)
        {
            if (id != prodaja_produkci.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodaja_produkci);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Prodaja_produkciExists(prodaja_produkci.ID))
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
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", prodaja_produkci.produkcia);
            ViewData["employee"] = new SelectList(_context.Employees, "ID", "FIO", prodaja_produkci.employee);
            return View(prodaja_produkci);
        }

        // GET: Prodaja_produkci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodaja_produkci = await _context.Prodaja_produkcis
                .Include(p => p.Employee)
                .Include(p => p.Produkcia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (prodaja_produkci == null)
            {
                return NotFound();
            }

            return View(prodaja_produkci);
        }

        // POST: Prodaja_produkci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodaja_produkci = await _context.Prodaja_produkcis.FindAsync(id);
            if (prodaja_produkci != null)
            {
                _context.Prodaja_produkcis.Remove(prodaja_produkci);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Prodaja_produkciExists(int id)
        {
            return _context.Prodaja_produkcis.Any(e => e.ID == id);
        }
    }
}
