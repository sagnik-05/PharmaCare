using PharmaAPI.Models;
using PharmaAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using PharmaAPI.Interface;

namespace PharmaAPI.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public InventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Fetch all drugs in inventory (with both ID and Name)
        public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            return await _context.Inventory.Include(i => i.Drug).ToListAsync();
        }

        // ✅ Fetch a specific drug from inventory by DrugId
        public async Task<Inventory> GetInventoryByDrugIdAsync(int drugId)
        {
            return await _context.Inventory.Include(i => i.Drug)
                                           .FirstOrDefaultAsync(i => i.DrugId == drugId);
        }

        // ✅ Fetch a specific drug from inventory by DrugName
        public async Task<Inventory> GetInventoryByDrugNameAsync(string drugName)
        {
            return await _context.Inventory.Include(i => i.Drug)
                                           .FirstOrDefaultAsync(i => i.DrugName == drugName);
        }

        // ✅ Add a drug to inventory using DrugName (API will auto-fetch DrugId)
        public async Task<bool> AddDrugToInventoryAsync(string drugName, int quantity)
        {
            var drug = await _context.Drugs.FirstOrDefaultAsync(d => d.Name == drugName);

            if (drug == null)
            {
                Console.WriteLine($"Drug '{drugName}' not found in the database.");
                return false; // Drug does not exist
            }

            // Check if the drug already exists in inventory
            var existingInventory = await _context.Inventory.FirstOrDefaultAsync(i => i.DrugId == drug.DrugId);

            if (existingInventory != null)
            {
                existingInventory.Quantity += quantity; // Update quantity if already exists
            }
            else
            {
                var inventoryItem = new Inventory
                {
                    DrugId = drug.DrugId,
                    DrugName = drug.Name,
                    Quantity = quantity
                };
                _context.Inventory.Add(inventoryItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Update drug stock quantity using DrugName
        public async Task<bool> UpdateDrugQuantityAsync(string drugName, int newQuantity)
        {
            var inventoryItem = await _context.Inventory.FirstOrDefaultAsync(i => i.DrugName == drugName);
            if (inventoryItem == null) return false;

            inventoryItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}