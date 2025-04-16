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
                .AsNoTracking()
                .Select(o => _MapToOrderDTO(o))
                .ToListAsync();
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Drug)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);

            return order != null ? _MapToOrderDTO(order) : null;
        }

        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO orderDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var drug = await _context.Drugs.FindAsync(orderDto.DrugId);
                if (drug == null)
                    throw new KeyNotFoundException("Drug not found.");

                if (!drug.Name.Equals(orderDto.DrugName, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Drug name does not match the selected Drug ID.");

                if (drug.Stock < orderDto.Quantity)
                    throw new InvalidOperationException("Insufficient stock available.");

                // Deduct stock
                drug.Stock -= orderDto.Quantity;

                // Create order
                var order = new Order
                {
                    DoctorId = orderDto.DoctorId,
                    DrugId = orderDto.DrugId,
                    Quantity = orderDto.Quantity,
                    Status = "Pending",
                    PlacedAt = DateTime.UtcNow
                };

                _context.Orders.Add(order);

                // Update or create sales entry
                var sale = await _context.Sales.FirstOrDefaultAsync(s => s.DrugId == orderDto.DrugId && s.Date == DateTime.UtcNow.Date);
                if (sale == null)
                {
                    sale = new Sales
                    {
                        DrugId = orderDto.DrugId,
                        Date = DateTime.UtcNow.Date,
                        TotalSales = orderDto.Quantity * drug.Price, 
                        FilePath = "sales_reports/" + DateTime.UtcNow.ToString("yyyyMMdd") + "_sales_report.csv" // Example file path
                    };
                    _context.Sales.Add(sale);
                }
                else
                {
                    sale.TotalSales += orderDto.Quantity * drug.Price;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return _MapToOrderDTO(order);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<OrderDTO?> UpdateOrderAsync(int id, UpdateOrderDTO orderDto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;
            order.Status = orderDto.Status;

            await _context.SaveChangesAsync();
            return _MapToOrderDTO(order);
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
                .AsNoTracking()
                .Where(o => o.DoctorId == doctorId)
                .Select(o => _MapToOrderDTO(o))
                .ToListAsync();
        }

        private static OrderDTO _MapToOrderDTO(Order order)
        {
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
    }
}
