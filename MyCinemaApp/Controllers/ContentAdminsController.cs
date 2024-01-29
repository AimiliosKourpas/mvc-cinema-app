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
    public class ContentAdminsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ContentAdminsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ContentAdmins
        public async Task<IActionResult> Index()
        {
            var myFirstMVCDBContext = _context.ContentAdmins.Include(c => c.UsernameNavigation);
            return View(await myFirstMVCDBContext.ToListAsync());
        }

        // GET: ContentAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .Include(c => c.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // GET: ContentAdmins/Create
        public IActionResult Create()
        {
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username");
            return View();
        }

        // POST: ContentAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Username")] ContentAdmin contentAdmin)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the corresponding User object based on the selected username
                User selectedUser = _context.Users.FirstOrDefault(u => u.Username == contentAdmin.Username);

                // Ensure a valid User object is found
                if (selectedUser != null)
                {
                    // Set the UsernameNavigation property
                    contentAdmin.UsernameNavigation = selectedUser;

                    // Add to context and save changes
                    _context.Add(contentAdmin);
                    await _context.SaveChangesAsync();

                    // Redirect to the Index action
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle the case where a valid User object is not found
                    ModelState.AddModelError("UsernameNavigation", "Selected username is not valid.");
                }
            }
            else
            {
                // Handle the case where the model is invalid
                ModelState.AddModelError("", "Invalid model.");
            }

            // Populate the dropdown with correct data
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", contentAdmin.Username);

            // Return to the view with the invalid model
            return View(contentAdmin);
        }


        // GET: ContentAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin == null)
            {
                return NotFound();
            }
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", contentAdmin.Username);
            return View(contentAdmin);
        }

        // POST: ContentAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Username")] ContentAdmin contentAdmin)
        {
            if (id != contentAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contentAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContentAdminExists(contentAdmin.Id))
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
            ViewData["Username"] = new SelectList(_context.Users, "Username", "Username", contentAdmin.Username);
            return View(contentAdmin);
        }

        // GET: ContentAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contentAdmin = await _context.ContentAdmins
                .Include(c => c.UsernameNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contentAdmin == null)
            {
                return NotFound();
            }

            return View(contentAdmin);
        }

        // POST: ContentAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contentAdmin = await _context.ContentAdmins.FindAsync(id);
            if (contentAdmin != null)
            {
                _context.ContentAdmins.Remove(contentAdmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContentAdminExists(int id)
        {
            return _context.ContentAdmins.Any(e => e.Id == id);
        }
    }
}

