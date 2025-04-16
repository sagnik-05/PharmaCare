using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTO;
using PharmaAPI.Interface;
using System;
using System.Threading.Tasks;

namespace PharmaAPI.Controllers;

[ApiController]
[Route("api/sales")]
public class SalesController : ControllerBase
{
    private readonly ISalesRepository _saleRepository; 

    public SalesController(ISalesRepository saleRepository) 
    {
        _saleRepository = saleRepository;
    }

    [HttpGet("view")]
    public async Task<IActionResult> GetSales()
    {
        try
        {
            var sales = await _saleRepository.GetSalesAsync();
            return Ok(sales);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpGet("view/{id}")]
    public async Task<IActionResult> GetSale(int id)
    {
        try
        {
            var sale = await _saleRepository.GetSaleAsync(id);
            if (sale == null)
            {
                return NotFound(new { Message = "Sale not found." });
            }
            return Ok(sale);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateSaleDTO updateSaleDTO)
    {
        try
        {
            var sale = await _saleRepository.UpdateSaleAsync(id, updateSaleDTO);
            if (sale == null)
            {
                return NotFound(new { Message = "Sale not found." });
            }
            return Ok(sale);
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

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        try
        {
            var result = await _saleRepository.DeleteSaleAsync(id);
            if (!result)
            {
                return NotFound(new { Message = "Sale not found." });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}