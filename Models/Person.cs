using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
        
        [Required]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champs Email doit etre remplis")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Le champs Mot de passe doit etre remplis")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public int? EmployeId { get; set; }
        public Employe? Employe { get; set; }
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
    }
}