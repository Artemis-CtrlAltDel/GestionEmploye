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
    public class EmployeesController : Controller
    {
        private readonly AppContext _context;

        public EmployeesController(AppContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            ViewData["Employees"] = await _context.Employe.Include(nameof(Person)).ToListAsync();
            return View();
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employe == null)
            {
                return NotFound();
            }

            var employe = await _context.Employe.Include(nameof(Person))
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (employe == null)
            {
                return NotFound();
            }

            ViewData["Employee"] = employe;

            return View(employe);
        }


        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Create(Employe employe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employe);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employe == null)
            {
                return NotFound();
            }

            var employe = await _context.Employe.Include(nameof(Person)).FirstOrDefaultAsync(i => i.Id == id);
            if (employe == null)
            {
                return NotFound();
            }
            return View(employe);
        }

        
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Employe employe)
        {
            if (id != employe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employe);
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Employe == null)
            {
                return Problem("Entity set 'AppContext.Employe'  is null.");
            }
            var employe = await _context.Employe.Include(nameof(Person)).FirstOrDefaultAsync(m => m.Id == id);
            if (employe != null)
            {
                _context.Person.Remove(employe.Person);
                _context.Employe.Remove(employe);
                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
