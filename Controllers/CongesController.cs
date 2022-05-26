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

        public async Task<IActionResult> Index()
        {
            ViewData["Conges"] = await _context.Conge.Include(c => c.Employe.Person).ToListAsync();
            return View();
        }

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

        public IActionResult Create()
        {
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id");
            return View();
        }

        [HttpPost]
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


        [HttpPost]
        public async Task<IActionResult> Accept(int id){
            var conge = await _context.Conge.FindAsync(id);
            if (conge.Status == "Pending"){
                conge.Status = "Accepted";
                _context.Update(conge);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Decline(int id){
            var conge = await _context.Conge.FindAsync(id);
            if (conge.Status == "Pending"){
                conge.Status = "Declined";
                _context.Update(conge);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
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
