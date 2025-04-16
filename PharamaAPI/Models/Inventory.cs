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
        public Drug Drug { get; set; }

        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
