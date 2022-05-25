using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestionEmploye.Models;

namespace GestionEmploye.Controllers;

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
        ViewData["TotalSalarie"] = _context.Salary.Where(m=> m.Month == DateTime.Now.Month).Count();
        ViewData["Employees"] = _context.Employe.Take(10).ToList();
        ViewData["PendingConges"] = _context.Conge.Where(m => m.Status == "Pending").Take(10).ToList();
        ViewData["CurrentConges"] = _context.Conge.Where(m => m.Status == "Accepted" && m.Date < DateTime.Now && m.Date.AddDays(m.Duration) > DateTime.Now).Take(10).ToList();
        ViewData["Salaries"] = _context.Salary.Take(10).ToList();

        return View();
    }
}
