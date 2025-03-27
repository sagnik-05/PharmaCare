using Microsoft.AspNetCore.Mvc;
using PharmaAPI.Interface;
using PharmaAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaAPI.Controllers
{
    [Route("api/inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryController(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            var inventory = await _inventoryRepository.GetAllInventoryAsync();
            return Ok(inventory);
        }

        
        [HttpGet("byId/{drugId}")]
        public async Task<IActionResult> GetDrugById(int drugId)
        {
            var inventoryItem = await _inventoryRepository.GetInventoryByDrugIdAsync(drugId);
            if (inventoryItem == null) return NotFound("Drug not found in inventory.");
            return Ok(inventoryItem);
        }

        
        [HttpGet("byName/{drugName}")]
        public async Task<IActionResult> GetDrugByName(string drugName)
        {
            var inventoryItem = await _inventoryRepository.GetInventoryByDrugNameAsync(drugName);
            if (inventoryItem == null) return NotFound("Drug not found in inventory.");
            return Ok(inventoryItem);
        }

    
        [HttpPost]
        public async Task<IActionResult> AddDrug([FromBody] InventoryDTO model)
        {
            if (string.IsNullOrEmpty(model.DrugName))
                return BadRequest("Drug name is required.");

            var success = await _inventoryRepository.AddDrugToInventoryAsync(model.DrugName, model.Quantity);

            if (!success)
                return BadRequest($"Drug '{model.DrugName}' not found in database. Please check if it exists.");

            return Ok($"Drug '{model.DrugName}' added to inventory successfully.");
        }


        [HttpPut("{drugName}")]
        public async Task<IActionResult> UpdateDrug(string drugName, [FromBody] int newQuantity)
        {
            var success = await _inventoryRepository.UpdateDrugQuantityAsync(drugName, newQuantity);
            if (!success) return NotFound("Drug not found in inventory.");
            return Ok("Drug updated successfully.");
        }
    }
}
