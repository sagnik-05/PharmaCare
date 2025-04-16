using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTO;
using PharmaAPI.Interface;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace PharmaAPI.Controllers
{
    [Route("api/drugs")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository _drugRepository;

        public DrugsController(IDrugRepository drugRepository)
        {
            _drugRepository = drugRepository;
        }
        [HttpGet("view")]
        public async Task<IActionResult> GetDrugs()
        {
            try
            {
                var drugs = await _drugRepository.GetDrugsAsync();
                return Ok(drugs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("view/{id}")]
        public async Task<IActionResult> GetDrug(int id)
        {
            try
            {
                var drug = await _drugRepository.GetDrugAsync(id);
                if (drug == null)
                    return NotFound(new { Message = "Drug not found." });

                return Ok(drug);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreateDrug([FromBody] CreateDrugDTO createDrugDTO)
        {
            try
            {
                var drug = await _drugRepository.CreateDrugAsync(createDrugDTO);
                return CreatedAtAction(nameof(GetDrug), new { id = drug.DrugId }, drug);
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateDrug(int id, [FromBody] UpdateDrugDTO updateDrugDTO)
        {
            try
            {
                var result = await _drugRepository.UpdateDrugAsync(id, updateDrugDTO);
                if (!result)
                    return NotFound(new { Message = "Drug not found." });

                return NoContent();
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
        public async Task<IActionResult> DeleteDrug(int id)
        {
            try
            {
                var result = await _drugRepository.DeleteDrugAsync(id);
                if (!result)
                    return NotFound(new { Message = "Drug not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost("request-drug")]
        //[Authorize(Roles = "Doctor")]
        public async Task<IActionResult> PostDrugRequest([FromBody] DrugRequestDTO model)
        {
            int requestId = await _drugRepository.PostDrugRequestAsync(model);
            int quantity = model.Quantity;
            return Ok(new { Message = "Drug request created successfully", RequestId = requestId, Quantity = quantity });
        }

        [HttpPost("approve-request")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveDrugRequest([FromBody] ApproveRequestDTO model)
        {

            return Ok(new { Message = "Drug request approved successfully", RequestId = model.RequestId });
        }
    }
}