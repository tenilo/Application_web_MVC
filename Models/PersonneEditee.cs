using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Appli.MVC.Entity.Models
{
    public class PersonneEditee : IValidatableObject
    {
        public int? ID { get; set; }
        [Required]
        public string Nom { get; set; }
        [Range(0, 100)]
        [Required]
        public int? Age { get; set; }
        [Required]
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var resultats = new List<ValidationResult>();
            if (String.IsNullOrWhiteSpace(Email) == false && Email.EndsWith(".com") == false)
            {
                var erreur = new ValidationResult(
                    "L'email ne termine pas par.com",
                    new List<string> { "Email" });

                resultats.Add(erreur);
            }
            return resultats;
        }
    }
}