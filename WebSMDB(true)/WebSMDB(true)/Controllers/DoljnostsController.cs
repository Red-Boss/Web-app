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
    public class DoljnostsController : Controller
    {
        private readonly AppDbContext _context;

        public DoljnostsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Doljnosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Doljnosts.ToListAsync());
        }

        // GET: Doljnosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doljnost = await _context.Doljnosts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doljnost == null)
            {
                return NotFound();
            }

            return View(doljnost);
        }

        // GET: Doljnosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doljnosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,name_doljnost")] Doljnost doljnost)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(doljnost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doljnost);
        }

        // GET: Doljnosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doljnost = await _context.Doljnosts.FindAsync(id);
            if (doljnost == null)
            {
                return NotFound();
            }
            return View(doljnost);
        }

        // POST: Doljnosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,name_doljnost")] Doljnost doljnost)
        {
            if (id != doljnost.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(doljnost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoljnostExists(doljnost.ID))
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
            return View(doljnost);
        }

        // GET: Doljnosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doljnost = await _context.Doljnosts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doljnost == null)
            {
                return NotFound();
            }

            return View(doljnost);
        }

        // POST: Doljnosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doljnost = await _context.Doljnosts.FindAsync(id);
            if (doljnost != null)
            {
                _context.Doljnosts.Remove(doljnost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoljnostExists(int id)
        {
            return _context.Doljnosts.Any(e => e.ID == id);
        }
    }
}
