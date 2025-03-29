using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTO;
using PharmaAPI.Interface;
using System.Threading.Tasks;

namespace PharmaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly ISalesRepository _saleRepository; // ✅ Inject ISaleRepository

    public SalesController(ISalesRepository saleRepository) 
    {
        _saleRepository = saleRepository;
    }

    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSales()
    {
        var sales = await _saleRepository.GetSalesAsync();
        return Ok(sales);
    }

    [HttpGet("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetSale(int id)
    {
        var sale = await _saleRepository.GetSaleAsync(id);
        if (sale == null)
        {
            return NotFound();
        }
        return Ok(sale);
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateSale([FromBody] CreateSaleDTO createSaleDTO)
    {
        var sale = await _saleRepository.CreateSaleAsync(createSaleDTO);
        if (sale == null)
        {
            return BadRequest("Failed to create sale.");
        }
        return Ok(sale);
    }

    [HttpPut("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateSaleDTO updateSaleDTO)
    {
        var sale = await _saleRepository.UpdateSaleAsync(id, updateSaleDTO);
        if (sale == null)
        {
            return NotFound();
        }
        return Ok(sale);
    }

    [HttpDelete("{id}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteSale(int id)
    {
        var result = await _saleRepository.DeleteSaleAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
