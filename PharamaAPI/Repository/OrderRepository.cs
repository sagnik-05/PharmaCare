using Microsoft.EntityFrameworkCore;
using PharmaAPI.Services;
using PharmaAPI.Models;
using PharmaAPI.Interface;
using PharmaAPI.DTOs;

namespace PharmaAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Drug)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    DoctorId = o.DoctorId,
                    DrugId = o.DrugId,
                    Quantity = o.Quantity,
                    Status = o.Status,
                    PlacedAt = o.PlacedAt
                })
                .ToListAsync();
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Drug)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null) return null;

            return new OrderDTO
            {
                Id = order.Id,
                DoctorId = order.DoctorId,
                DrugId = order.DrugId,
                Quantity = order.Quantity,
                Status = order.Status,
                PlacedAt = order.PlacedAt
            };
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO orderDto)
        {
            var order = new Order
            {
                DoctorId = orderDto.DoctorId,
                DrugId = orderDto.DrugId,
                Quantity = orderDto.Quantity
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDTO
            {
                Id = order.Id,
                DoctorId = order.DoctorId,
                DrugId = order.DrugId,
                Quantity = order.Quantity,
                Status = order.Status,
                PlacedAt = order.PlacedAt
            };
        }

        public async Task<OrderDTO?> UpdateOrderAsync(int id, UpdateOrderDTO orderDto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            order.Quantity = orderDto.Quantity;
            order.Status = orderDto.Status;

            await _context.SaveChangesAsync();

            return new OrderDTO
            {
                Id = order.Id,
                DoctorId = order.DoctorId,
                DrugId = order.DrugId,
                Quantity = order.Quantity,
                Status = order.Status,
                PlacedAt = order.PlacedAt
            };
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByDoctorIdAsync(int doctorId)
        {
            return await _context.Orders
                .Include(o => o.Drug)
                .Where(o => o.DoctorId == doctorId)
                .Select(o => new OrderDTO
                {
                    Id = o.Id,
                    DoctorId = o.DoctorId,
                    DrugId = o.DrugId,
                    Quantity = o.Quantity,
                    Status = o.Status,
                    PlacedAt = o.PlacedAt
                })
                .ToListAsync();
        }
    }
}
