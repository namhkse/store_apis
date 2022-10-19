using Microsoft.AspNetCore.Mvc;
using store_api.Services;

namespace store_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public IActionResult FindOrder([FromRoute] int id)
        {
            var order = _orderService.FindOrder(id);

            return (order is null)
                ? NotFound()
                : Ok(order);
        }

        [HttpGet]
        public IActionResult GetOrders([FromQuery] int? page)
        {
            int n = page ?? 1;
            var orders = _orderService.GetOrdersWithPaging(n, 10);
            return Ok(orders);
        }
    }
}