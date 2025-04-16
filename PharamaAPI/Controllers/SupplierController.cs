using Microsoft.AspNetCore.Mvc;
using PharmaAPI.Interface;
using PharmaAPI.DTOs;

namespace PharmaAPI.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierController(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers()
        {
            try
            {
                var suppliers = await _supplierRepository.GetAllSuppliersAsync();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddSupplier([FromBody] CreateSupplierDTO supplierDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var supplier = await _supplierRepository.AddSupplierAsync(supplierDto);
                return CreatedAtAction(nameof(GetSuppliers), new { id = supplier.Id }, supplier);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { Message = ex.Message });
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