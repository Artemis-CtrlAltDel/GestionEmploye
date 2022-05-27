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
                HttpContext.Session.SetInt32("PersonId", result.Person.Id);
                HttpContext.Session.SetString("Nom", result.Person.Nom);
                HttpContext.Session.SetString("Prenom", result.Person.Prenom);
                HttpContext.Session.SetInt32("Admin", 1);
                Redirect("/");
            }

            var result1 = await _context.Employe.Include(nameof(Employe.Person)).FirstOrDefaultAsync(m => m.Person.Email == person.Email);
            if (result1 == null)
            {
                ModelState.AddModelError("Email", "Email non trouvé!");
                return View();
            }
            if (result1.Person.Password != person.Password)
            {
                ModelState.AddModelError("Password", "Mot de passe erroné!");
                return View();
            }
            HttpContext.Session.SetInt32("PersonId", result1.Person.Id);
            HttpContext.Session.SetString("Nom", result1.Person.Nom);
            HttpContext.Session.SetString("Prenom", result1.Person.Prenom);
            HttpContext.Session.SetInt32("Admin", 0);
            return Redirect("/");
        }

        [Route("/logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
      
    }
}
