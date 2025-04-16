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

        // Fetch all inventory data with Drug & Supplier details
        public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            return await _context.Inventory
                .Include(i => i.Drug)
                .Include(i => i.Supplier)
                .ToListAsync();
        }

        // Fetch inventory by Drug ID
        public async Task<Inventory> GetInventoryByDrugIdAsync(int drugId)
        {
            return await _context.Inventory
                .Include(i => i.Drug)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.DrugId == drugId);
        }

        // Fetch inventory by Drug Name
        public async Task<Inventory> GetInventoryByDrugNameAsync(string drugName)
        {
            return await _context.Inventory
                .Include(i => i.Drug)
                .Include(i => i.Supplier)
                .FirstOrDefaultAsync(i => i.Drug.Name == drugName);
        }

        // Add a drug to inventory (Ensure Supplier & Drug exist)
        public async Task<bool> AddDrugToInventoryAsync(string drugName, int supplierId, int quantity)
        {
            var drug = await _context.Drugs.FirstOrDefaultAsync(d => d.Name == drugName);
            if (drug == null) return false;

            var supplier = await _context.Suppliers.FindAsync(supplierId);
            if (supplier == null) return false;

            var existingInventory = await _context.Inventory
                .FirstOrDefaultAsync(i => i.DrugId == drug.DrugId && i.SupplierId == supplierId);

            if (existingInventory != null)
            {
                existingInventory.Quantity += quantity;
            }
            else
            {
                var inventoryItem = new Inventory
                {
                    DrugId = drug.DrugId,
                    SupplierId = supplierId,
                    Quantity = drug.Stock + quantity
                };
                _context.Inventory.Add(inventoryItem);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        // Update existing drug stock in inventory
        public async Task<bool> UpdateDrugQuantityAsync(string drugName, int newQuantity)
        {
            var inventoryItem = await _context.Inventory
                .Include(i => i.Drug)
                .FirstOrDefaultAsync(i => i.Drug.Name == drugName);

            if (inventoryItem == null) return false;

            inventoryItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();
            return true;
        }
        // keyword search for drug names in inventory
        public async Task<List<Inventory>> GetKeywordAsync(string drugName)
        {
            return await _context.Inventory.Include(i => i.Drug)
                                        .Where(i => i.Drug.Name.StartsWith(drugName))
                                        .ToListAsync();
        }
        // delete drug
        public async Task<bool> DeleteDrugFromInventoryAsync(int drugId)
        {
            var inventoryItem = await _context.Inventory.FindAsync(drugId);
            if (inventoryItem == null) return false;

            _context.Inventory.Remove(inventoryItem);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
