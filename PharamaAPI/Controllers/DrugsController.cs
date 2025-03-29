using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTO;
using PharmaAPI.Interface;
using System.Threading.Tasks;

namespace PharmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository _drugRepository;

        public DrugsController(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetDrugs()
        {
            var drugs = await _drugRepository.GetDrugsAsync();
            return Ok(drugs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrug(int id)
        {
            var drug = await _drugRepository.GetDrugAsync(id);
            if (drug == null)
                return NotFound();
            return Ok(drug);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateDrug([FromBody] CreateDrugDTO createDrugDTO)
        {
            var drug = await _drugRepository.CreateDrugAsync(createDrugDTO);
            return CreatedAtAction(nameof(GetDrug), new { id = drug.DrugId }, drug);
        }

        [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDrug(int id, [FromBody] UpdateDrugDTO updateDrugDTO)
        {
            var result = await _drugRepository.UpdateDrugAsync(id, updateDrugDTO);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDrug(int id)
        {
            var result = await _drugRepository.DeleteDrugAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
