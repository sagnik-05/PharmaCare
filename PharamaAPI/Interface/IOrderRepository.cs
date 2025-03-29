using PharmaAPI.DTOs;

namespace PharmaAPI.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO orderDto);
        Task<OrderDTO?> UpdateOrderAsync(int id, UpdateOrderDTO orderDto);
        Task<bool> DeleteOrderAsync(int id);
        Task<IEnumerable<OrderDTO>> GetOrdersByDoctorIdAsync(int doctorId);
    }
}
