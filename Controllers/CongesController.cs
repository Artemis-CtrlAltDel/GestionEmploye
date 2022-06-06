using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Models;
using GestionEmploye.Helpers;

namespace GestionEmploye.Controllers
{
    public class CongesController : Controller
    {
        private readonly AppContext _context;

        public CongesController(AppContext context)
        {
            _context = context;
        }

        [LoggedInFilter]
        public async Task<IActionResult> Index()
        {
            if(HttpContext.Session.GetInt32("Admin") == 1){
                ViewData["Conges"] = await _context.Conge.Include(c => c.Employe.Person).ToListAsync();
            }else{
                ViewData["Conges"] = await _context.Conge.Where(m => m.EmployeId == HttpContext.Session.GetInt32("EmployeId")).ToListAsync();
                ViewData["Employee"] = await _context.Employe.FindAsync(HttpContext.Session.GetInt32("EmployeId"));
            }

            return View();
        }

        [LoggedInFilter]
        [HttpPost("/conges")]
        public async Task<IActionResult> Create([Bind("Date,Duration")] Conge conge)
        {
            conge.Status = "Pending";
            var EmployeId = HttpContext.Session.GetInt32("EmployeId");
            if(EmployeId == null) return Problem();
            var employe = _context.Employe.Find(EmployeId);
            if(employe.CongeRemaining < conge.Duration){
                ViewData["Employee"] = await _context.Employe.FirstOrDefaultAsync(i => i.Id == HttpContext.Session.GetInt32("EmployeId"));
                ViewData["Conges"] = await _context.Conge.Where(m => m.EmployeId == HttpContext.Session.GetInt32("EmployeId")).ToListAsync();
                ModelState.AddModelError("Duration",$"Vous avez {employe.CongeRemaining} jours restants");
                return View("Index");
            }
            
            conge.EmployeId = (int)EmployeId;
            if (ModelState.IsValid)
            {
                employe.CongeRemaining -= conge.Duration;
                _context.Add(conge);
                _context.Update(employe);
                await _context.SaveChangesAsync();
            }
            ViewData["Employee"] = await _context.Employe.FirstOrDefaultAsync(i => i.Id == HttpContext.Session.GetInt32("EmployeId"));
            ViewData["Conges"] = await _context.Conge.Where(m => m.EmployeId == HttpContext.Session.GetInt32("EmployeId")).ToListAsync();
            return View("Index");
        }

        [AdminOnlyFilter]
        [HttpPost]
        public async Task<IActionResult> Accept(int id){
            var conge = await _context.Conge.FindAsync(id);
            if (conge?.Status == "Pending"){
                conge.Status = "Accepted";
                _context.Update(conge);
                await _context.SaveChangesAsync();
            }
            return Redirect(Request.Headers.Referer);
        }

        [AdminOnlyFilter]
        [HttpPost]
        public async Task<IActionResult> Decline(int id){
            var conge = await _context.Conge.FindAsync(id);
            if (conge.Status == "Pending"){
                conge.Status = "Declined";
                var emp = await _context.Employe.FindAsync(conge.EmployeId);
                emp.CongeRemaining += conge.Duration;
                _context.Update(conge);
                await _context.SaveChangesAsync();
            }
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var conge = await _context.Conge.FindAsync(id);
            if(HttpContext.Session.GetInt32("Admin") != 1 && (HttpContext.Session.GetInt32("EmployeId") != conge.EmployeId)) {
                return Redirect("/conges");
            }
            if (conge != null)
            {
                var emp = await _context.Employe.FindAsync(conge.EmployeId);
                emp.CongeRemaining += conge.Duration;
                _context.Conge.Remove(conge);
            }
            
            await _context.SaveChangesAsync();
            return Redirect(Request.Headers.Referer);
        }

        [HttpPost]
        [AdminOnlyFilter]
        public async Task<IActionResult> Reset(){
            var employees = await _context.Employe.ToListAsync();
            employees.ForEach(e => e.CongeRemaining = 30);
            await _context.SaveChangesAsync();
            return Redirect("/");
        }

    }
}
