using PharmaAPI.Interface;
using PharmaAPI.Models;
using PharmaAPI.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using PharmaAPI.Services;

namespace PharmaAPI.Repository
{
    public class DrugRepository : IDrugRepository
    {
        private readonly ApplicationDbContext _context;

        public DrugRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch all drugs
        public async Task<IEnumerable<DrugDTO>> GetDrugsAsync()
        {
            return await _context.Drugs.Select(d => new DrugDTO
            {
                DrugId = d.DrugId,
                Name = d.Name,
                Manufacturer = d.Manufacturer,
                Price = d.Price,
                Stock = d.Stock
            }).ToListAsync();
        }
        // Fetch drug by ID

        public async Task<DrugDTO> GetDrugAsync(int drugId)
        {
            var drug = await _context.Drugs.FindAsync(drugId);
            if (drug == null)
                return null;

            return new DrugDTO
            {
                DrugId = drug.DrugId,
                Name = drug.Name,
                Manufacturer = drug.Manufacturer,
                Price = drug.Price,
                Stock = drug.Stock
            };
        }
        // Create a new drug
        public async Task<DrugDTO> CreateDrugAsync(CreateDrugDTO createDrugDTO)
        {
            var drug = new Drug
            {
                Name = createDrugDTO.Name,
                Manufacturer = createDrugDTO.Manufacturer,
                Price = createDrugDTO.Price,
                Stock = createDrugDTO.Stock
            };

            _context.Drugs.Add(drug);
            await _context.SaveChangesAsync();

            return new DrugDTO
            {
                DrugId = drug.DrugId,
                Name = drug.Name,
                Manufacturer = drug.Manufacturer,
                Price = drug.Price,
                Stock = drug.Stock
            };
        }
        // Update an existing drug
        public async Task<bool> UpdateDrugAsync(int drugId, UpdateDrugDTO updateDrugDTO)
        {
            var drug = await _context.Drugs.FindAsync(drugId);
            if (drug == null)
                return false;

            drug.Name = updateDrugDTO.Name;
            drug.Manufacturer = updateDrugDTO.Manufacturer;
            drug.Price = updateDrugDTO.Price;
            drug.Stock = updateDrugDTO.Stock;

            _context.Entry(drug).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrugExists(drugId))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }
        // Delete a drug
        public async Task<bool> DeleteDrugAsync(int drugId)
        {
            var drug = await _context.Drugs.FindAsync(drugId);
            if (drug == null)
                return false;

            _context.Drugs.Remove(drug);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool DrugExists(int drugId)
        {
            return _context.Drugs.Any(e => e.DrugId == drugId);
        }
        public async Task<int> PostDrugRequestAsync(DrugRequestDTO model)
        {
            var request = new DrugRequest
            {
                RequestId = new Random().Next(1000, 9999), // Generate random 4-digit ID
                DrugName = model.DrugName,
                Quantity = model.Quantity
            };

            _context.Add(request);
            return await Task.FromResult(request.RequestId); // Return generated ID
        }

        public async Task<bool> ApproveDrugRequestAsync(int requestId)
        {
            return true;
        }
    }
}
