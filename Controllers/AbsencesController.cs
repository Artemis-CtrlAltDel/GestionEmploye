using Microsoft.AspNetCore.Mvc;
using GestionEmploye.Models;
using Microsoft.EntityFrameworkCore;
using GestionEmploye.Helpers;



public class AbsencesController : Controller
{

    private readonly AppContext _context;

    public AbsencesController(AppContext context)
    {
        _context = context;
    }

    [AdminOnlyFilter]
    public async Task<IActionResult> Absent(int Id){
        Employe employe = await _context.Employe.Include(i=>i.Absences).FirstOrDefaultAsync(i => i.Id == Id);
        if(employe != null){
            var abs = employe.Absences.FirstOrDefault(i=>i.Date.ToString("MM/dd/yyyy") == DateTime.Now.ToString("MM/dd/yyyy"));
            if(abs == null){
                employe.Absences.Add(new Absence(){ Date = DateTime.Now });
                _context.SaveChanges();
            }else{
                employe.Absences.Remove(abs);
                _context.SaveChanges();
            }

        }
        return Redirect(Request.Headers.Referer);
    }

}