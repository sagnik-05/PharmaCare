using System;
using System.ComponentModel.DataAnnotations;

namespace PharmaAPI.Models
{
    public class DrugRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        public string DrugName { get; set; }
        [Required]
        public int Quantity { get; set; }

        public bool IsApproved { get; set; } = false;
    }
}
