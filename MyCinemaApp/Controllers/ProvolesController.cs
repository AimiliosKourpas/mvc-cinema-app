using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var myFirstMVCDBContext = _context.Provoles.Include(p => p.Cinemas).Include(p => p.ContentAdmin).Include(p => p.Movie);
            return View(await myFirstMVCDBContext.ToListAsync());
        }

        // GET: Provoles/Details/5
        public async Task<IActionResult> Details(int? moviesId, int? cinemasId, string moviesName)
        {
            if (moviesId == null || cinemasId == null || moviesName == null)
            {
                return NotFound();
            }

            var provole = await _context.Provoles
                .Include(p => p.Cinemas)
                .Include(p => p.ContentAdmin)
                .Include(p => p.Movie)
                .FirstOrDefaultAsync(m => m.MoviesId == moviesId && m.CinemasId == cinemasId && m.MoviesName == moviesName);

            if (provole == null)
            {
                return NotFound();
            }

            return View(provole);
        }


        // GET: Provoles/Create
        public IActionResult Create()
        {
            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Name");
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Username");
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Id");
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name");
            return View();
        }

        // POST: Provoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoviesId,MoviesName,CinemasId,MovieDate,ContentAdminId,Id")] Provole provole)
        {
            if (ModelState.IsValid)
            {

                _context.Add(provole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Debug.WriteLine("Model state is invalid. Errors:");

                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    if (state.Errors.Any())
                    {
                        Debug.WriteLine($"Key: {key}, Errors: {string.Join(", ", state.Errors.Select(e => e.ErrorMessage))}");
                    }
                }
            }

            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Name", provole.CinemasId);
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Username", provole.ContentAdminId);
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name", provole.MoviesId);
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name", provole.MoviesName);

            return View(provole);
        }

        // GET: Provoles/Edit/5
        public async Task<IActionResult> Edit(int moviesId, int cinemasId, string moviesName)
        {
            var provole = await _context.Provoles.FindAsync(moviesId, cinemasId, moviesName);

            if (provole == null)
            {
                return NotFound();
            }

            ViewData["CinemasList"] = new SelectList(_context.Cinemas, "Id", "Name", provole.CinemasId);
            ViewData["ContentAdminList"] = new SelectList(_context.ContentAdmins, "Id", "Username", provole.ContentAdminId);
            ViewData["MovieList"] = new SelectList(_context.Movies, "Id", "Name", provole.MoviesId);
            ViewData["MovieNameList"] = new SelectList(_context.Movies, "Name", "Name", provole.MoviesName);

            return View(provole);
        }

        // POST: Provoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int moviesId, int cinemasId, string moviesName, Provole provole)
        {
            if (moviesId != provole.MoviesId || cinemasId != provole.CinemasId || moviesName != provole.MoviesName)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(provole).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency exception if needed
                    throw;
                }
            }

            // Repopulate SelectList if ModelState is not valid
            ViewData["CinemasId"] = new SelectList(_context.Cinemas, "Id", "Name", provole.CinemasId);
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Username", provole.ContentAdminId);
            ViewData["MoviesId"] = new SelectList(_context.Movies, "Id", "Name", provole.MoviesId);
            ViewData["MoviesName"] = new SelectList(_context.Movies, "Name", "Name", provole.MoviesName);

            return View(provole);
        }


        // GET: Provoles/Delete/5
        public async Task<IActionResult> Delete(int? moviesId, int? cinemasId, string moviesName)
        {
            if (moviesId == null || cinemasId == null || moviesName == null)
            {
                return NotFound();
            }

            var provole = await _context.Provoles
                .Include(p => p.Cinemas)
                .Include(p => p.ContentAdmin)
                .Include(p => p.Movie)
                .FirstOrDefaultAsync(m => m.MoviesId == moviesId && m.CinemasId == cinemasId && m.MoviesName == moviesName);

            if (provole == null)
            {
                return NotFound();
            }

            return View(provole);
        }

        // POST: Provoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int moviesId, int cinemasId, string moviesName)
        {
            var provole = await _context.Provoles
                .FirstOrDefaultAsync(m => m.MoviesId == moviesId && m.CinemasId == cinemasId && m.MoviesName == moviesName);

            if (provole == null)
            {
                return NotFound();
            }

            _context.Provoles.Remove(provole);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvoleExists(int id)
        {
            return _context.Provoles.Any(e => e.MoviesId == id);
        }
    }
}

