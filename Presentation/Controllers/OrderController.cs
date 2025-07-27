using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IServiceManager _serviceManager) : ControllerBase
    {
        // POST: api/order/create
        [HttpPost("create")]
        public async Task<ActionResult<OrderResponse>> CreateOrderAsync([FromBody] OrderRequest orderRequest)
        {
            var result = await _serviceManager.OrderService.CreateAsync(orderRequest);
            return CreatedAtAction(nameof(GetOrderByIdAsync), new { orderId = result.Id }, result);
        }

        // GET: api/order
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetAll()
        {
            var orders = await _serviceManager.OrderService.GetAllAsync();
            return Ok(orders);
        }

        // GET: api/order/customer/5
        [HttpGet("customer/{customerId:int}")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _serviceManager.OrderService.GetOrdersByIdAsync(customerId);
            return Ok(orders);
        }

        // GET: api/order/5
        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<OrderResponse>> GetOrderByIdAsync(int orderId)
        {
            var allOrders = await _serviceManager.OrderService.GetAllAsync();
            var order = allOrders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
                return NotFound($"Order with ID {orderId} not found.");

            return Ok(order);
        }

        // PUT: api/order/5/status
        [HttpPut("{orderId:int}/status")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateOrderStatusAsync(int orderId, [FromBody] string status)
        {
            try
            {
                await _serviceManager.OrderService.UpdateStatusAsync(orderId, status);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
