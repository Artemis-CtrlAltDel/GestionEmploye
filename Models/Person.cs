using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le champ Nom doit etre remplis")]
        public string Nom { get; set; }
        
        [Required(ErrorMessage = "Le champ Prenom doit etre remplis")]
        public string Prenom { get; set; }

        [Required(ErrorMessage = "Le champ Email doit etre remplis")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Le champ Mot de passe doit etre remplis")]
        [DataType(DataType.Password)]
        [MinLength(6,ErrorMessage = "La longueur minimale pour un password est 6 caractères")]
        public string Password { get; set; }

        public int? EmployeId { get; set; }
        public Employe? Employe { get; set; }
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
    }
}