using Microsoft.EntityFrameworkCore;
using PharmaAPI.Services;
using PharmaAPI.Models;
using PharmaAPI.Interface;
using PharmaAPI.DTOs;

namespace PharmaAPI.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // Fetch all suppliers
        public async Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync()
        {
            return await _context.Suppliers
                .AsNoTracking()
                .Select(s => new SupplierDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    Email = s.Email
                })
                .ToListAsync();
        }
        // Fetch supplier by ID
        public async Task<SupplierDTO> AddSupplierAsync(CreateSupplierDTO supplierDto)
        {
            if (await _context.Suppliers.AnyAsync(s => s.Email == supplierDto.Email))
                throw new InvalidOperationException("Supplier with this email already exists.");

            var supplier = new Supplier
            {
                Name = supplierDto.Name,
                Email = supplierDto.Email
            };

            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            return new SupplierDTO
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Email = supplier.Email
            };
        }
    }
}
