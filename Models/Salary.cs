using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Salary {
        
        public int Id { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        [Range(0,11)]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [RegularExpression("Pending|Paid", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

        
        [Required]
        public int EmployeId {get; set;}

        [Required]
        public Employe Employe {get; set;}

    }
}