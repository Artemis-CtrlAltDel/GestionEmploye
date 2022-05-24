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

        // [HttpPost("/login/{*type}")]
        // public IActionResult Login(string type) {

        //     return View();
        // }


        // todo: remove this after finishing debuging
        //      ||
        //      ||
        //     \  /
        //      \/

    }
}
