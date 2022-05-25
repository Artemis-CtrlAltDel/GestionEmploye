using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Conge
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DemandeTime { get; set; }

        [Range(1,30)]
        [Required]
        public int Duration { get; set; }

        [DefaultValue(0)]
        [RegularExpression("Pending|Accepted|Declined", ErrorMessage = "Invalid Status")]
        public string Status { get; set; }

        [Required]
        public int EmployeId {get; set;}

        public Employe Employe {get; set;}

    }
}