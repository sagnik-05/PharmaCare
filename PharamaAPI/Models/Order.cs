using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PharmaAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [ForeignKey("Drug")]
        public int DrugId { get; set; }
        public Drug Drug { get; set; }
    }
}