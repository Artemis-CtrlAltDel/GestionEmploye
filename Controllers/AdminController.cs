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
        
        return View();
    }
}
