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

        public async Task<bool> UpdateDrugAsync(int drugId, UpdateDrugDTO updateDrugDTO)
        {
            var drug = await _context.Drugs.FindAsync(drugId);
            if (drug == null)
                return false;

            drug.Name = updateDrugDTO.Name;
            drug.Manufacturer = updateDrugDTO.Manufacturer;
            drug.Price = decimal.Parse(updateDrugDTO.Price);
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
    }
}