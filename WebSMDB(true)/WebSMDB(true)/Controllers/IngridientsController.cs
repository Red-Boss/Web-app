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
    public class IngridientsController : Controller
    {
        private readonly AppDbContext _context;

        public IngridientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ingridients
        public async Task<IActionResult> Index(int? produkciaFilter)
        {
            var appDbContext = _context.Ingridients.Include(i => i.Syrio);
            // Если есть фильтр по produkcia, применяем его
            if (produkciaFilter==null)
            {
                produkciaFilter= _context.Produkcias.First().ID;
            }
            var appDbContext2 = appDbContext.Where(i => i.produkcia == produkciaFilter);
            // Отсортировать элементы в ViewData["produkcia"] по значению "ID"
            //ViewData["produkcia"] = new SelectList(_context.Produkcias.OrderBy(p => p.ID == produkciaFilter).ThenBy(p => p.ID), "ID", "Naimenovanie", produkciaFilter);
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", produkciaFilter);
            ViewBag.produkciaId = produkciaFilter;
            return View(await appDbContext2.ToListAsync());
        }

        // GET: Ingridients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingridient = await _context.Ingridients
                .Include(i => i.Syrio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ingridient == null)
            {
                return NotFound();
            }

            ViewBag.produkciaId = _context.Produkcias.Find(ingridient.produkcia);

            return View(ingridient);

        }

        // GET: Ingridients/Create
        public IActionResult Create(int? produkciaId)
        {
            if (produkciaId == null)
            {
                produkciaId= _context.Produkcias.First().ID;
            }

            ViewBag.produkciaId = _context.Produkcias.Find(produkciaId);
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala");
            return View();
        }

        // POST: Ingridients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int produkciaId, [Bind("ID,produkcia,syrio,Count")] Ingridient ingridient)
        {
            

            // Удалим ошибку по ключу "syrio"
            ModelState.Remove("syrio");

            if (ModelState.IsValid)
            {
                // Проверяем, существует ли ингредиент с такими же параметрами
                var existingIngredient = await _context.Ingridients.FirstOrDefaultAsync(i => i.produkcia == ingridient.produkcia && i.syrio == ingridient.syrio);
                if (existingIngredient == null)
                {
                    // Ингредиент не найден, добавляем новый
                    _context.Add(ingridient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { produkciaFilter = ingridient.produkcia });
                }
                else
                {
                    // Ингредиент уже существует, вы можете выбрать другие действия, например, отобразить сообщение об ошибке
                    ModelState.AddModelError(string.Empty, "Такой ингредиент существует.");
                }
            }

            // Обновим ViewBag.produkciaId
            ViewBag.produkciaId = _context.Produkcias.Find(ingridient.produkcia);

            // Обновим SelectList для syrio
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala", ingridient.syrio);

            return View(ingridient);
        }


        // GET: Ingridients/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingridient = await _context.Ingridients.FindAsync(id);
            ViewBag.produkciaId = _context.Produkcias.Find(ingridient.produkcia);
            if (ingridient == null)
            {
                return NotFound();
            }
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", ingridient.produkcia);
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala", ingridient.syrio);

            return View(ingridient);
        }

        // POST: Ingridients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,produkcia,syrio,Count")] Ingridient ingridient)
        {
            if (id != ingridient.ID)
            {
                return NotFound();
            }
            ViewBag.produkciaId = _context.Produkcias.Find(ingridient.produkcia);
            ViewData["produkcia"] = new SelectList(_context.Produkcias, "ID", "Naimenovanie", ingridient.produkcia);
            ViewData["syrio"] = new SelectList(_context.Syrios, "ID", "Naimenovanie_materiala", ingridient.syrio);
            // Удалим ошибку по ключу "syrio"
            ModelState.Remove("syrio");
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingridient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngridientExists(ingridient.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new {produkciaFilter= ingridient.produkcia });
            }
            return View(ingridient);
        }

        // GET: Ingridients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingridient = await _context.Ingridients
                .Include(i => i.Syrio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (ingridient == null)
            {
                return NotFound();
            }

            ViewBag.produkciaId = _context.Produkcias.Find(ingridient.produkcia);
            
            return View(ingridient);
        }

        // POST: Ingridients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingridient = await _context.Ingridients.FindAsync(id);
            if (ingridient != null)
            {
                _context.Ingridients.Remove(ingridient);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { produkciaFilter = ingridient.produkcia });
        }

        private bool IngridientExists(int id)
        {
            return _context.Ingridients.Any(e => e.ID == id);
        }
    }
}
