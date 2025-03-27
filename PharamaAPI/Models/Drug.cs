using System.ComponentModel.DataAnnotations;

namespace PharmaAPI.Models
{
    public class Drug
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public string Manufacturer { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; } // Tracks available quantity
    }
}
