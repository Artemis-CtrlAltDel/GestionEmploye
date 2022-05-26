using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Models;

namespace GestionEmploye.Controllers
{
    public class SalariesController : Controller
    {
        private readonly AppContext _context;

        public SalariesController(AppContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int dupErr = 0)
        {
            var employees = 
                _context.Employe
                    .Select(s => new 
                    { 
                    Id = s.Id,
                    FullName = $"{s.Id} - {s.Person.Nom} {s.Person.Prenom} ({s.CurrentSalary} DH)"
                    })
                    .ToList();

            ViewData["EmployeId"] = new SelectList(employees, "Id", "FullName");
            var status = new [] { new {Name = "En cours" , Value = "Pending"},new {Name = "Payé" , Value = "Paid"}}.ToList();
            ViewData["StatusList"] = new SelectList(status, "Value", "Name");
            ViewData["Salaries"] = await _context.Salary.Include(m => m.Employe.Person).ToListAsync();
            if(dupErr == 1){
                ModelState.AddModelError(string.Empty,"Paiement déjà effectué");
            }
            return View();
        }

        

        public IActionResult Create()
        {
            ViewData["EmployeId"] = new SelectList(_context.Employe, "Id", "Id");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Salary salary)
        {
            salary.Amount = _context.Employe.Find(salary.EmployeId).CurrentSalary;
            if (await _context.Salary.AnyAsync(m => m.Month == salary.Month && m.EmployeId == salary.EmployeId)){
                return Redirect("/salaries?dupErr=1");
            }

            if (ModelState.IsValid)
            {
                _context.Add(salary);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        
        
        [HttpPost]
        public async Task<IActionResult> Paid(int id)
        {
            var salary = await _context.Salary.FindAsync(id);
            if(salary != null){
                salary.Status = "Paid";
                _context.Update(salary);
                await _context.SaveChangesAsync();
            }
            return Redirect("/salaries#lst");
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            
            var salary = await _context.Salary.FindAsync(id);
            if (salary != null)
            {
                _context.Salary.Remove(salary);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
