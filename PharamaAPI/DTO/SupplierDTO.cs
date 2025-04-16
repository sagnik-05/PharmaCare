using System.ComponentModel.DataAnnotations;

namespace PharmaAPI.DTOs
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public class CreateSupplierDTO
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
