using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Salary {
        
        public int Id { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Month { get; set; }

        [Required]
        [RegularExpression("Pending|Paid", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

        
        [Required]
        public int EmployeId {get; set;}

        public Employe Employe {get; set;}

    }
}