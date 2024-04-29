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
    public class ProdukciasController : Controller
    {
        private readonly AppDbContext _context;

        public ProdukciasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produkcias
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Produkcias.Include(p => p.Edinica_izmerenia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Produkcias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkcia = await _context.Produkcias
                .Include(p => p.Edinica_izmerenia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produkcia == null)
            {
                return NotFound();
            }

            return View(produkcia);
        }

        // GET: Produkcias/Create
        public IActionResult Create()
        {
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie");
            return View();
        }

        // POST: Produkcias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naimenovanie,edinica_izmerenia,Count,Sum")] Produkcia produkcia)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(produkcia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie", produkcia.edinica_izmerenia);
            return View(produkcia);
        }

        // GET: Produkcias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkcia = await _context.Produkcias.FindAsync(id);
            if (produkcia == null)
            {
                return NotFound();
            }
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie", produkcia.edinica_izmerenia);
            return View(produkcia);
        }

        // POST: Produkcias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naimenovanie,edinica_izmerenia,Count,Sum")] Produkcia produkcia)
        {
            if (id != produkcia.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkcia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdukciaExists(produkcia.ID))
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
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie", produkcia.edinica_izmerenia);
            return View(produkcia);
        }

        // GET: Produkcias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produkcia = await _context.Produkcias
                .Include(p => p.Edinica_izmerenia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (produkcia == null)
            {
                return NotFound();
            }

            return View(produkcia);
        }

        // POST: Produkcias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produkcia = await _context.Produkcias.FindAsync(id);
            if (produkcia != null)
            {
                _context.Produkcias.Remove(produkcia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdukciaExists(int id)
        {
            return _context.Produkcias.Any(e => e.ID == id);
        }
    }
}
