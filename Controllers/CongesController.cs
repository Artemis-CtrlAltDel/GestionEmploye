using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Models;

namespace GestionEmploye.Controllers
{
    public class CongesController : Controller
    {
        private readonly AppContext _context;

        public CongesController(AppContext context)
        {
            _context = context;
        }

        // GET: Conges
        public async Task<IActionResult> Index()
        {
            ViewData["Conges"] = await _context.Conge.Include(c => c.Employe.Person).ToListAsync();
            return View();
        }

        // GET: Conges/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Conge == null)
            {
                return NotFound();
            }

            var conge = await _context.Conge
                .Include(c => c.Employe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conge == null)
            {
                return NotFound();
            }

            return View(conge);
        }

        // GET: Conges/Create
        public IActionResult Create()
        {
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id");
            return View();
        }

        // POST: Conges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,DemandeTime,Duration,Status,EmployeId")] Conge conge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id", conge.EmployeId);
            return View(conge);
        }

        // GET: Conges/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Conge == null)
            {
                return NotFound();
            }

            var conge = await _context.Conge.FindAsync(id);
            if (conge == null)
            {
                return NotFound();
            }
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id", conge.EmployeId);
            return View(conge);
        }

        // POST: Conges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,DemandeTime,Duration,Status,EmployeId")] Conge conge)
        {
            if (id != conge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongeExists(conge.Id))
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
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id", conge.EmployeId);
            return View(conge);
        }

        // GET: Conges/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Conge == null)
            {
                return NotFound();
            }

            var conge = await _context.Conge
                .Include(c => c.Employe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (conge == null)
            {
                return NotFound();
            }

            return View(conge);
        }

        // POST: Conges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Conge == null)
            {
                return Problem("Entity set 'AppContext.Conge'  is null.");
            }
            var conge = await _context.Conge.FindAsync(id);
            if (conge != null)
            {
                _context.Conge.Remove(conge);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongeExists(int id)
        {
          return _context.Conge.Any(e => e.Id == id);
        }
    }
}
