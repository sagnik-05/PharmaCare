using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaAPI.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public Drug Drug { get; set; } // Navigation property

        [Required]
        public string DrugName { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
