using PharmaAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaAPI.Interface
{
    public interface IDrugRepository
    {
        Task<IEnumerable<DrugDTO>> GetDrugsAsync();
        Task<DrugDTO> GetDrugAsync(int drugId);
        Task<DrugDTO> CreateDrugAsync(CreateDrugDTO createDrugDTO);
        Task<bool> UpdateDrugAsync(int drugId, UpdateDrugDTO updateDrugDTO);
        Task<bool> DeleteDrugAsync(int drugId);
    }
}
