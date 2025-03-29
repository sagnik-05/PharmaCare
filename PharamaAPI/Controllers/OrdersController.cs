using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTOs;
using PharmaAPI.Interface;

namespace PharmaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrder(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            
            return Ok(order);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrdersByDoctor(int doctorId)
        {
            var orders = await _orderRepository.GetOrdersByDoctorIdAsync(doctorId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var createdOrder = await _orderRepository.CreateOrderAsync(createOrderDTO);
            return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder(int id, [FromBody] UpdateOrderDTO updateOrderDTO)
        {
            var updatedOrder = await _orderRepository.UpdateOrderAsync(id, updateOrderDTO);
            if (updatedOrder == null)
                return NotFound();

            return Ok(updatedOrder);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderRepository.DeleteOrderAsync(id);
            if (!result)
                return NotFound();
            
            return NoContent();
        }
    }
}