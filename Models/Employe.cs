using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Employe
    {
        public int Id { get; set; }

        [Required]
        public int PersonId {get; set;}

        [Required]
        public Person Person {get; set;}

        
        [DefaultValue(30)]
        [Range(0,30)]
        public int CongeRemaining { get; set; }

        [Display(Name = "Salaire")]
        [RegularExpression("\\d+", ErrorMessage = "Le salaire doit etre numerique")]
        [Required(ErrorMessage = "Le champ Salaire doit etre remplis")]
        public float CurrentSalary { get; set; }

        public ICollection<Conge> Conges { get; set; }
        
        public ICollection<Salary> Salaries { get; set; }
    }
}