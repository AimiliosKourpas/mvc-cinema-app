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
    public class MoviesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MoviesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var myFirstMVCDBContext = _context.Movies.Include(m => m.ContentAdmin);
            return View(await myFirstMVCDBContext.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.ContentAdmin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Content,Length,Type,Summary,Director,ContentAdminId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the corresponding ContentAdmin object based on the selected ContentAdminId
                ContentAdmin selectedContentAdmin = _context.ContentAdmins.FirstOrDefault(ca => ca.Id == movie.ContentAdminId);

                // Ensure a valid ContentAdmin object is found
                if (selectedContentAdmin != null)
                {
                    // Set the ContentAdmin navigation property
                    movie.ContentAdmin = selectedContentAdmin;

                    // Add to context and save changes
                    _context.Add(movie);
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the case where a valid ContentAdmin object is not found
                    ModelState.AddModelError("ContentAdmin", "Selected ContentAdminId is not valid.");
                }
            }
            else
            {
                // Handle the case where the model state is invalid
                ModelState.AddModelError("", "Model state is invalid.");
            }

            // Populate the dropdown with correct data
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", movie.ContentAdminId);

            // Return to the view with the invalid model
            return View(movie);
        }


        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Debug.WriteLine("Id is null");
                return NotFound();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                Debug.WriteLine("Movie is null");
                return NotFound();
            }

            Debug.WriteLine("ContentAdminId: " + movie.ContentAdminId);
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", movie.ContentAdminId);
            return View(movie);
        }


        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,Length,Type,Summary,Director,ContentAdminId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
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
            ViewData["ContentAdminId"] = new SelectList(_context.ContentAdmins, "Id", "Id", movie.ContentAdminId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.ContentAdmin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}





