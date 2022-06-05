using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Absence
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public int EmployeId {get; set;}

        public Employe Employe {get; set;}

    }
}