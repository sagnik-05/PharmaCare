using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaAPI.Models
{
    public class Sales
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public decimal TotalSales { get; set; }
        [Required]
        public string? FilePath { get; set; }
        [Required]
        public int DrugId { get; set; }

        [ForeignKey("DrugId")]
        public Drug Drug { get; set; }
    }
}
