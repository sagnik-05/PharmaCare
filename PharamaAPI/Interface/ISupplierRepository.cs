using PharmaAPI.DTOs;

namespace PharmaAPI.Interface
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<SupplierDTO>> GetAllSuppliersAsync();
        Task<SupplierDTO> AddSupplierAsync(CreateSupplierDTO supplierDto);
    }
}
