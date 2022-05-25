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
    public class AuthController : Controller
    {
        private readonly AppContext _context;

        public AuthController(AppContext context)
        {
            _context = context;
        }


        [HttpGet("/login/{*type}")]
        public IActionResult Login(string type)
        {
            return View();
        }

        [HttpPost("/login/{*type}")]
        public async Task<IActionResult> Login(string type, [Bind("Email,Password")] Person person)
        {
            if (type == "admin")
            {
                var result = await _context.Admin.Include(nameof(Admin.Person)).FirstOrDefaultAsync(m => m.Person.Email == person.Email);
                if (result == null)
                {
                    ModelState.AddModelError("Email", "Email not found!");
                    return View();
                }
                if (result.Person.Password != person.Password)
                {
                    ModelState.AddModelError("Password", "Wrong password!");
                    return View();
                }
                HttpContext.Session.SetInt32("PersonId", result.PersonId);
                HttpContext.Session.SetString("Nom", result.Person.Nom);
                HttpContext.Session.SetString("Prenom", result.Person.Prenom);
                HttpContext.Session.SetInt32("Admin", 1);
                Redirect("/");
            }

            var result1 = await _context.Employe.Include(nameof(Employe.Person)).FirstOrDefaultAsync(m => m.Person.Email == person.Email);
            if (result1 == null)
            {
                ModelState.AddModelError("Email", "Email not found!");
                return View();
            }
            if (result1.Person.Password != person.Password)
            {
                ModelState.AddModelError("Password", "Wrong password!");
                return View();
            }
            HttpContext.Session.SetInt32("PersonId", result1.PersonId);
            HttpContext.Session.SetString("Nom", result1.Person.Nom);
            HttpContext.Session.SetString("Prenom", result1.Person.Prenom);
            HttpContext.Session.SetInt32("Admin", 0);
            return Redirect("/");
        }


        // todo: remove this after finishing debuging
        //      ||
        //      ||
        //     \  /
        //      \/


        // GET: Auth
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Person.Include(p => p.Admin).Include(p => p.Employe);
            return View(await appContext.ToListAsync());
        }

        // GET: Auth/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Admin)
                .Include(p => p.Employe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Auth/Create
        public IActionResult Create()
        {
            ViewData["EmployeId"] = new SelectList(_context.Admin, "Id", "Id");
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id");
            return View();
        }

        // POST: Auth/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Password,EmployeId,AdminId")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeId"] = new SelectList(_context.Admin, "Id", "Id", person.EmployeId);
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id", person.EmployeId);
            return View(person);
        }

        // GET: Auth/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["EmployeId"] = new SelectList(_context.Admin, "Id", "Id", person.EmployeId);
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id", person.EmployeId);
            return View(person);
        }

        // POST: Auth/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Password,EmployeId,AdminId")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
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
            ViewData["EmployeId"] = new SelectList(_context.Admin, "Id", "Id", person.EmployeId);
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id", person.EmployeId);
            return View(person);
        }

        // GET: Auth/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .Include(p => p.Admin)
                .Include(p => p.Employe)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Auth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Person == null)
            {
                return Problem("Entity set 'AppContext.Person'  is null.");
            }
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return (_context.Person?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
