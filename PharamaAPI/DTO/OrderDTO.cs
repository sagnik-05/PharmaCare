// DTOs/OrderDTO.cs
namespace PharmaAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DrugId { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public DateTime PlacedAt { get; set; }
    }

    public class CreateOrderDTO
    {
        public int DoctorId { get; set; }
        public int DrugId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateOrderDTO
    {
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}