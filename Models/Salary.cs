using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Salary
    {

        public int Id { get; set; }

        [Display(Name = "Montant")]
        [Required]
        public float Amount { get; set; }

        [Display(Name = "Mois")]
        [Required(ErrorMessage = "Le champ Mois doit etre remplis")]
        [DataType(DataType.Date)]
        public DateTime Month { get; set; }

        [Required]
        [RegularExpression("Pending|Paid", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }


        [Required]
        [Display(Name = "Employe")]
        public int EmployeId { get; set; }

        public Employe Employe { get; set; }

    }
}