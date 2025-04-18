using PharmaAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaAPI.Interface
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAllInventoryAsync();
        Task<Inventory> GetInventoryByDrugIdAsync(int drugId);
        Task<Inventory> GetInventoryByDrugNameAsync(string drugName);
        Task<bool> AddDrugToInventoryAsync(string drugName, int supplierId, int quantity);
        Task<bool> UpdateDrugQuantityAsync(string drugName, int newQuantity);
        Task<List<Inventory>> GetKeywordAsync(string drugName);
        Task<bool> DeleteDrugFromInventoryAsync(int drugId);
    }
}
