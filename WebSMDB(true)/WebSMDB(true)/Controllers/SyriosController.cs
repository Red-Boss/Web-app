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
    public class SyriosController : Controller
    {
        private readonly AppDbContext _context;

        public SyriosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Syrios
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Syrios.Include(s => s.Edinica_izmerenia);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Syrios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var syrio = await _context.Syrios
                .Include(s => s.Edinica_izmerenia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (syrio == null)
            {
                return NotFound();
            }

            return View(syrio);
        }

        // GET: Syrios/Create
        public IActionResult Create()
        {
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie");
            return View();
        }

        // POST: Syrios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naimenovanie_materiala,edinica_izmerenia,Count,Sum")] Syrio syrio)
        {
            if (!ModelState.IsValid)
            {
                var existing = await _context.Syrios.FirstOrDefaultAsync(i => i.Naimenovanie_materiala == syrio.Naimenovanie_materiala);
                if (existing==null)
                {
                    _context.Add(syrio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Ингредиент уже существует, вы можете выбрать другие действия, например, отобразить сообщение об ошибке
                    ModelState.AddModelError(string.Empty, "Такое сырьё уже существует.");
                }
            }
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie", syrio.edinica_izmerenia);
            return View(syrio);
        }

        // GET: Syrios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var syrio = await _context.Syrios.FindAsync(id);
            if (syrio == null)
            {
                return NotFound();
            }
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie", syrio.edinica_izmerenia);
            return View(syrio);
        }

        // POST: Syrios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naimenovanie_materiala,edinica_izmerenia,Count,Sum")] Syrio syrio)
        {
            if (id != syrio.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(syrio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SyrioExists(syrio.ID))
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
            ViewData["edinica_izmerenia"] = new SelectList(_context.Edinica_izmerenias, "ID", "Naimenovanie", syrio.edinica_izmerenia);
            return View(syrio);
        }

        // GET: Syrios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var syrio = await _context.Syrios
                .Include(s => s.Edinica_izmerenia)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (syrio == null)
            {
                return NotFound();
            }

            return View(syrio);
        }

        // POST: Syrios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var syrio = await _context.Syrios.FindAsync(id);
            if (syrio != null)
            {
                _context.Syrios.Remove(syrio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SyrioExists(int id)
        {
            return _context.Syrios.Any(e => e.ID == id);
        }
    }
}
