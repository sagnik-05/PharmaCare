using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTOs;
using PharmaAPI.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PharmaAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]

    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("view")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("view/{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order == null)
                    return NotFound(new { Message = "Order not found." });

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByDoctor(int doctorId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByDoctorIdAsync(doctorId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPost]
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            try
            {
                var createdOrder = await _orderRepository.CreateOrderAsync(createOrderDTO);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
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

        [HttpPut("status/update/{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            try
            {
                var updatedOrder = await _orderRepository.UpdateOrderAsync(id, updateOrderDTO);
                if (updatedOrder == null)
                    return NotFound(new { Message = "Order not found." });

                return Ok(updatedOrder);
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
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var result = await _orderRepository.DeleteOrderAsync(id);
                if (!result)
                    return NotFound(new { Message = "Order not found." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}