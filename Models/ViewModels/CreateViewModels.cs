using System.ComponentModel.DataAnnotations;

namespace tp2.Models.ViewModels
{
    public class CreateViewModels
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Désignation { get; set; }

        [Required]
        [Display(Name = "Prix en dinar :")]
        public float Prix { get; set; }

        [Required]
        [Display(Name = "Quantité en unité :")]
        public int Quantite { get; set; }
        [Required]
        [Display(Name = "Image :")]

        public IFormFile ImagePath { get; set; }
    }
}
