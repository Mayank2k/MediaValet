using MediaValet.OrderSupervisor.Repository.DTOs;
using MediaValet.OrderSupervisor.Repository.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.Agent
{
   public class App
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<App> _logger;
        private readonly Guid _agentId;
        private readonly int _magicNumber;

        private int retryCount = 3;
        private readonly TimeSpan delay = TimeSpan.FromSeconds(5);

        public App(IOrderRepository orderRepository, ILogger<App> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
            _agentId = Guid.NewGuid();
            _magicNumber = new Random().Next(1, 10);
        }

        public async Task Run()
        {
            Console.WriteLine($"I'm agent {_agentId}, my magic number is {_magicNumber} !");
            while (true)
            {
                var order = await _orderRepository.GetOrder();
                
                if (order == null) continue;
                
                Console.WriteLine($"Received order {order.OrderId}");
                await ProcessOrdersAsync(order);

                if (order.MagicNumber != _magicNumber) continue;
                Console.WriteLine("Oh no, my magic number was found !");
                break;
            }

            Console.ReadLine();
        }

        
        // retry logic - 3
        // poision queue-log message those are failed to process after 3 attampts 
        private async Task ProcessOrdersAsync(OrderDTO order)
        {
            int currentRetry = 0;
            for (; ; )
            {
                try
                {   
                    await _orderRepository.SaveOrder(order, _agentId);
                    await _orderRepository.RemoveOrder(order);

                    _logger.LogInformation($"Sucessfully processed Order {order.OrderId} with agent {_agentId} !");
                    break;
                }
                catch (Exception)
                {
                    _logger.LogWarning("Operation Exception");

                    currentRetry++;
                    if (currentRetry > this.retryCount)
                    {
                        _logger.LogError($"Unable to process Order {order.OrderId} with agent {_agentId} !"); 
                    }
                }
                await Task.Delay(delay);
            }
        }
    }
}
