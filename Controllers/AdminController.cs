using Microsoft.AspNetCore.Mvc;
using GestionEmploye.Models;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Helpers;

namespace GestionEmploye.Controllers;


[AdminOnlyFilter]
public class AdminController : Controller
{
    private readonly AppContext _context;

    public AdminController(AppContext context)
    {
        _context = context;
    }

    public IActionResult Index() {
        ViewData["TotalEmployees"] = _context.Employe.Count();
        ViewData["TotalDemandeConges"] = _context.Conge.Where(m=> m.Status == "Pending").Count();
        ViewData["TotalSalaries"] = _context.Salary.Where(m=> m.Month.Month == DateTime.Now.Month).Count();
        ViewData["Employees"] = _context.Employe.Include(nameof(Person)).Take(10).ToList();
        ViewData["PendingConges"] = _context.Conge.Where(m => m.Status == "Pending").Take(10).ToList();
        ViewData["CurrentConges"] = _context.Conge.Where(m => m.Status == "Accepted" && m.Date < DateTime.Now && m.Date.AddDays(m.Duration) > DateTime.Now).Take(10).ToList();
        ViewData["Salaries"] = _context.Salary.Take(10).ToList();
        ViewData["Absences"] = _context.Absence.Take(10).ToList();

        return View();
    }


    public string Create(){
        _context.Admin
        .Add(new Admin() {
            Person = new Person(){
                Nom = "Admin",
                Prenom = "Admin",
                Email = "admin@gmail.com",
                Password = "adminadmin",
            }
        });
        _context.SaveChanges();
        return "done";
    }
}
