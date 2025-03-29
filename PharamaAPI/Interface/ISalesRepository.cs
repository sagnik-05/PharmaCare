using PharmaAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaAPI.Interface
{
    public interface ISalesRepository
    {
        Task<IEnumerable<SaleDTO>> GetSalesAsync();
        Task<SaleDTO> GetSaleAsync(int saleId);
        Task<SaleDTO> CreateSaleAsync(CreateSaleDTO createSaleDTO);
        Task<SaleDTO> UpdateSaleAsync(int saleId, UpdateSaleDTO updateSaleDTO);
        Task<bool> DeleteSaleAsync(int saleId); 
    }
}
