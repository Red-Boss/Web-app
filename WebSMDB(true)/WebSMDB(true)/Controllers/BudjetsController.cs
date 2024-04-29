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
    public class BudjetsController : Controller
    {
        private readonly AppDbContext _context;

        public BudjetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Budjets
        public async Task<IActionResult> Index()
        {
            return View(await _context.Budjets.ToListAsync());
        }

        // GET: Budjets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budjet = await _context.Budjets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (budjet == null)
            {
                return NotFound();
            }

            return View(budjet);
        }

        // GET: Budjets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Budjets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,budjet")] Budjet Budjets)
        {
            if (ModelState.IsValid)
            {
                var existingBudjet = _context.Budjets.FirstOrDefault();
                existingBudjet.budjet += Budjets.budjet;

                try
                {
                    _context.Update(existingBudjet);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudjetExists(existingBudjet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(Budjets);
        }

        // GET: Budjets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Budjets = await _context.Budjets.FindAsync(id);
            if (Budjets == null)
            {
                return NotFound();
            }
            return View(Budjets);
        }

        // POST: Budjets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,budjet,bonus,pr_prodaji")] Budjet Budjets)
        {
            /*if (id != budjet.ID)
            {
                return NotFound();
            }*/
            Budjets.ID = id;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Budjets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BudjetExists(Budjets.ID))
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
            return View(Budjets);
        }

        // GET: Budjets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budjet = await _context.Budjets
                .FirstOrDefaultAsync(m => m.ID == id);
            if (budjet == null)
            {
                return NotFound();
            }

            return View(budjet);
        }

        // POST: Budjets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var budjet = await _context.Budjets.FindAsync(id);
            if (budjet != null)
            {
                _context.Budjets.Remove(budjet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BudjetExists(int id)
        {
            return _context.Budjets.Any(e => e.ID == id);
        }
    }
}
