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
    public class Edinica_izmereniaController : Controller
    {
        private readonly AppDbContext _context;

        public Edinica_izmereniaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Edinica_izmerenia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Edinica_izmerenias.ToListAsync());
        }

        // GET: Edinica_izmerenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edinica_izmerenia = await _context.Edinica_izmerenias
                .FirstOrDefaultAsync(m => m.ID == id);
            if (edinica_izmerenia == null)
            {
                return NotFound();
            }

            return View(edinica_izmerenia);
        }

        // GET: Edinica_izmerenia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Edinica_izmerenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Naimenovanie")] Edinica_izmerenia edinica_izmerenia)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(edinica_izmerenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(edinica_izmerenia);
        }

        // GET: Edinica_izmerenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edinica_izmerenia = await _context.Edinica_izmerenias.FindAsync(id);
            if (edinica_izmerenia == null)
            {
                return NotFound();
            }
            return View(edinica_izmerenia);
        }

        // POST: Edinica_izmerenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Naimenovanie")] Edinica_izmerenia edinica_izmerenia)
        {
            if (id != edinica_izmerenia.ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(edinica_izmerenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Edinica_izmereniaExists(edinica_izmerenia.ID))
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
            return View(edinica_izmerenia);
        }

        // GET: Edinica_izmerenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edinica_izmerenia = await _context.Edinica_izmerenias
                .FirstOrDefaultAsync(m => m.ID == id);
            if (edinica_izmerenia == null)
            {
                return NotFound();
            }

            return View(edinica_izmerenia);
        }

        // POST: Edinica_izmerenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var edinica_izmerenia = await _context.Edinica_izmerenias.FindAsync(id);
            if (edinica_izmerenia != null)
            {
                _context.Edinica_izmerenias.Remove(edinica_izmerenia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Edinica_izmereniaExists(int id)
        {
            return _context.Edinica_izmerenias.Any(e => e.ID == id);
        }
    }
}
