using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpPost("addorder")]
        public IActionResult AddOrder(OrderDTO model)
        {
            try
            {
                Order order = new()
                {
                    K205UserId = model.K205UserId,  
                    ProductId = model.ProductId,
                    TotalPrice = model.TotalPrice,
                    TotalQuantity = model.TotalQuantity,
                    OrderTrackingId = model.OrderTrackingId,
                    IsDelivered = model.IsDelivered,
                };
                _orderManager.Add(order);

                return Ok(new {status = 200, message = "Sifaris tamamlandi." });
            }
            catch (Exception)
            {
                return BadRequest(new { status = 403, message = "Sifaris zamani xeta bas verdi." });
            }

        }   
    }
}
