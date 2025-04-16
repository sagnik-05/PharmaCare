using Microsoft.AspNetCore.Mvc;
using PharmaAPI.Interface;
using PharmaAPI.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("view")]
        public async Task<IActionResult> GetAllInventory()
        {
            try
            {
                var inventory = await _inventoryRepository.GetAllInventoryAsync();
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("byId/{drugId}")]
        public async Task<IActionResult> GetDrugById(int drugId)
        {
            try
            {
                var inventoryItem = await _inventoryRepository.GetInventoryByDrugIdAsync(drugId);
                if (inventoryItem == null) return NotFound(new { Message = "Drug not found in inventory." });
                return Ok(inventoryItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
        [HttpGet("byName/{drugName}")]
        public async Task<IActionResult> GetDrugByName(string drugName)
        {
            try
            {
                var inventoryItem = await _inventoryRepository.GetInventoryByDrugNameAsync(drugName);
                if (inventoryItem == null) return NotFound(new { Message = "Drug not found in inventory." });
                return Ok(inventoryItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
        [HttpGet("search/keyword/{drugName}")]
        public async Task<IActionResult> GetDrugsByKeyword(string drugName)
        {
            var inventoryItems = await _inventoryRepository.GetKeywordAsync(drugName);
            if (inventoryItems == null || !inventoryItems.Any())
                return NotFound("No drugs found with the given prefix in inventory.");

            return Ok(inventoryItems);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddDrug([FromBody] InventoryDTO model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.DrugName))
                    return BadRequest(new { Message = "Drug name is required." });

                var success = await _inventoryRepository.AddDrugToInventoryAsync(model.DrugName, model.SupplierId, model.Quantity);

                if (!success)
                    return BadRequest(new { Message = $"Drug '{model.DrugName}' or Supplier not found in database." });

                return Ok(new { Message = $"Drug '{model.DrugName}' added to inventory successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("update/{drugName}")]
        public async Task<IActionResult> UpdateDrug(string drugName, [FromBody] int newQuantity)
        {
            try
            {
                var success = await _inventoryRepository.UpdateDrugQuantityAsync(drugName, newQuantity);
                if (!success) return NotFound(new { Message = "Drug not found in inventory." });
                return Ok(new { Message = "Drug stock updated successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
        [HttpDelete("delete/{drugId}")]
        public async Task<IActionResult> DeleteDrugFromInventory(int drugId)
        {
            try
            {
                var success = await _inventoryRepository.DeleteDrugFromInventoryAsync(drugId);
                if (!success)
                    return NotFound(new { Message = "Drug not found in inventory." });

                return Ok(new { Message = "Drug deleted from inventory successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}