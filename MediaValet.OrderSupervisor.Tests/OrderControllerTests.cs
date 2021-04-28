using MediaValet.OrderSupervisor.API.Controllers;
using MediaValet.OrderSupervisor.Repository.DTOs;
using MediaValet.OrderSupervisor.Repository.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.Tests
{
    [TestClass]
    public class OrderControllerTests
    {
        [TestMethod]
        public void OrderController_Get_Test()
        {
            var mock = new Mock<IOrderRepository>();
            var logger = new Mock<ILogger<OrderController>>();

            OrderDTO o = new OrderDTO() { OrderId = 1, OrderText = "test", MagicNumber = 1, QueueId = "1", Receipt = "1" };
            mock.Setup(p => p.GetOrder()).ReturnsAsync(o);

            OrderController orderController = new OrderController(mock.Object, logger.Object);
            var result = orderController.Get().Result;

            Assert.AreEqual(1, result.OrderId);
        }

        [TestMethod]
        public void OrderController_Post_Test()
        {
            var mock = new Mock<IOrderRepository>();
            var logger = new Mock<ILogger<OrderController>>();

            OrderDTO o = new OrderDTO() { OrderId = 1, OrderText = "test", MagicNumber = 1, QueueId = "1", Receipt = "1" };
            var input = "test";
            mock.Setup(p => p.AddOrder(input)).ReturnsAsync(o);

            OrderController orderController = new OrderController(mock.Object, logger.Object);
            var result = orderController.Post(input).Result;

            Assert.AreEqual("Send order 1 with random number 1", ((ObjectResult)result).Value);
        }
    }
}
