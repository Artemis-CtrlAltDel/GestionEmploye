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

    }
}