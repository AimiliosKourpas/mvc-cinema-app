using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyCinemaApp.Models;

namespace MyCinemaApp.Controllers
{
    public class ProvolesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProvolesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Provoles
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Provoles.Include(p => p.Cinemas).Include(p => p.ContentAdmin).Include(p => p.Movie);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Provoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provole = await _context.Provoles
                .Include(p => p.Cinemas)
                .Include(p => p.ContentAdmin)
                .Include(p => p.Movie)
                .FirstOrDefaultAsync(m => m.MoviesId == id);
            if (provole == null)
            {
                return NotFound();
            }

            return View(provole);
        }

        // GET: Provoles/Create
        public IActionResult Create()
        {
            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Id");
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id");
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name");
            return View();
        }

        // POST: Provoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoviesId,CinemasId,MoviesName,ContentAdminId,Id")] Provole provole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Id", provole.CinemasId);
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", provole.ContentAdminId);
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name", provole.MoviesId);
            return View(provole);
        }

        // GET: Provoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provole = await _context.Provoles.FindAsync(id);
            if (provole == null)
            {
                return NotFound();
            }
            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Id", provole.CinemasId);
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", provole.ContentAdminId);
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name", provole.MoviesId);
            return View(provole);
        }

        // POST: Provoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MoviesId,CinemasId,MoviesName,ContentAdminId,Id")] Provole provole)
        {
            if (id != provole.MoviesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(provole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvoleExists(provole.MoviesId))
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
            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Id", provole.CinemasId);
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", provole.ContentAdminId);
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name", provole.MoviesId);
            return View(provole);
        }

        // GET: Provoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provole = await _context.Provoles
                .Include(p => p.Cinemas)
                .Include(p => p.ContentAdmin)
                .Include(p => p.Movie)
                .FirstOrDefaultAsync(m => m.MoviesId == id);
            if (provole == null)
            {
                return NotFound();
            }

            return View(provole);
        }

        // POST: Provoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provole = await _context.Provoles.FindAsync(id);
            if (provole != null)
            {
                _context.Provoles.Remove(provole);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvoleExists(int id)
        {
            return _context.Provoles.Any(e => e.MoviesId == id);
        }
    }
}
