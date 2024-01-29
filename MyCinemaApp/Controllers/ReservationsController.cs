using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MyCinemaApp.Models;

namespace MyCinemaApp.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ReservationsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var myFirstMVCDBContext = _context.Reservations.Include(r => r.Customers).Include(r => r.Provole);
            return View(await myFirstMVCDBContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? moviesId, int? cinemasId, int? customersId)
        {
            if (moviesId == null || cinemasId == null || customersId == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customers)
                .Include(r => r.Provole)
                .FirstOrDefaultAsync(m => m.MoviesId == moviesId && m.CinemasId == cinemasId && m.CustomersId == customersId);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create()
        {
            ViewData["CustomersId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["MoviesId"] = new SelectList(_context.Provoles, "MoviesId", "MoviesId");

            var cinemas = _context.Provoles.Select(p => p.Cinemas).Distinct().ToList();
            ViewData["CinemasId"] = new SelectList(cinemas, "Id", "Name");

            ViewData["MoviesName"] = new SelectList(_context.Provoles, "MoviesName", "MoviesName");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoviesId,MoviesName,CinemasId,CustomersId,NumberOfSeats")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewData["CustomersId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["MoviesId"] = new SelectList(_context.Provoles, "MoviesId", "MoviesId");
            ViewData["CinemasId"] = new SelectList(_context.Provoles, "CinemasId", "CinemasId");
            ViewData["MoviesName"] = new SelectList(_context.Provoles, "MoviesName", "MoviesName");
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? moviesId, int? cinemasId, int? customersId)
        {
            if (moviesId == null || cinemasId == null || customersId == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(moviesId, cinemasId, customersId);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["CustomersId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["MoviesId"] = new SelectList(_context.Provoles, "MoviesId", "MoviesId");
            ViewData["CinemasId"] = new SelectList(_context.Provoles, "CinemasId", "CinemasId");
            ViewData["MoviesName"] = new SelectList(_context.Provoles, "MoviesName", "MoviesName");
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int moviesId, int cinemasId, int customersId, [Bind("MoviesId,MoviesName,CinemasId,CustomersId,NumberOfSeats")] Reservation reservation)
        {
            Debug.WriteLine($"moviesId: {moviesId}, cinemasId: {cinemasId}, customersId: {customersId}");
            if (moviesId != reservation.MoviesId || cinemasId != reservation.CinemasId || customersId != reservation.CustomersId)
            {
                Debug.WriteLine("ID mismatch between route parameter and model key properties");
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.MoviesId))
                    {
                        Debug.WriteLine("Reservation does not exist.");
                        return NotFound();
                    }
                    else
                    {
                        Debug.WriteLine("DbUpdateConcurrencyException");
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Debug.WriteLine(error.ErrorMessage);
                    }
                }
            }
            ViewData["CustomersId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["MoviesId"] = new SelectList(_context.Provoles, "MoviesId", "MoviesId");
            ViewData["CinemasId"] = new SelectList(_context.Provoles, "CinemasId", "CinemasId");
            ViewData["MoviesName"] = new SelectList(_context.Provoles, "MoviesName", "MoviesName");
            return View(reservation);
        }

        // GET: Reservations/Delete
        public async Task<IActionResult> Delete(int? moviesId, int? cinemasId, int? customersId)
        {
            if (moviesId == null || cinemasId == null || customersId == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Customers)
                .Include(r => r.Provole)
                .FirstOrDefaultAsync(m => m.MoviesId == moviesId && m.CinemasId == cinemasId && m.CustomersId == customersId);

            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int moviesId, int cinemasId, int customersId)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(m => m.MoviesId == moviesId && m.CinemasId == cinemasId && m.CustomersId == customersId);

            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Debug.WriteLine("Reservation does not exist.");
                return NotFound();
            }
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.MoviesId == id);
        }
    }
}
