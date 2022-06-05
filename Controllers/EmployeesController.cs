using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Models;
using GestionEmploye.Helpers;

namespace GestionEmploye.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppContext _context;

        public EmployeesController(AppContext context)
        {
            _context = context;
        }

        [AdminOnlyFilter]
        public async Task<IActionResult> Index()
        {
            ViewData["Employees"] = await _context.Employe.Include(nameof(Person)).ToListAsync();
            return View();
        }

        [LoggedInFilter]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employe == null)
            {
                return NotFound();
            }

            var employe = await _context.Employe.Include(nameof(Person)).Include(m=> m.Conges).Include(m=> m.Salaries).Include(m=> m.Absences)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (employe == null)
            {
                return NotFound();
            }

            ViewData["Employee"] = employe;
            ViewData["TotalAbsences"] = employe.Absences.Count();
            ViewData["TotalAbsencesThisMonth"] = employe.Absences.Where(i=>i.Date.ToString("MM/yyyy") == DateTime.Now.ToString("MM/yyyy")).Count();

            return View(employe);
        }

        [AdminOnlyFilter]
        public IActionResult Create()
        {
            return View();
        }

        [AdminOnlyFilter]
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

        [LoggedInFilter]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != HttpContext.Session.GetInt32("EmployeId"))
            {
                return Redirect("/");
            }


            var employe = await _context.Employe.Include(nameof(Person)).FirstOrDefaultAsync(i => i.Id == id);
            if (employe == null)
            {
                return NotFound();
            }
            return View(employe);
        }

        [LoggedInFilter]
        [HttpPost]
        public async Task<IActionResult> Edit(int id,Employe employe)
        {
            if (id != employe.Id || id != HttpContext.Session.GetInt32("EmployeId"))
            {
                return Redirect("/");
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

        [AdminOnlyFilter]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
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
