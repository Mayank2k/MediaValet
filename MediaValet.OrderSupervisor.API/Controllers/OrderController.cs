using MediaValet.OrderSupervisor.Repository.DTOs;
using MediaValet.OrderSupervisor.Repository.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.API.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<OrderDTO> Get()
        {
            var result = await _orderRepository.GetOrder();
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string orderText)
        {
            try
            {
                var result = await _orderRepository.AddOrder(orderText);
                return Ok($"Send order {result.OrderId} with random number {result.MagicNumber}");
            }
            catch (Exception)
            {
                _logger.LogError("Post method falied !");
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError);
            }
        }
    }
}
