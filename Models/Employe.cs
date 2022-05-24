using System.ComponentModel.DataAnnotations;

namespace GestionEmploye.Models
{
    public class Employe
    {
        public int Id { get; set; }

        [Required]
        public string Nom { get; set; }
        public string Prenom { get; set; }

    }
}