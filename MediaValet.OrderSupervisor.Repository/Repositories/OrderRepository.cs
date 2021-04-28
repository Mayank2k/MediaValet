using AutoMapper;
using MediaValet.OrderSupervisor.Repository.DTOs;
using MediaValet.OrderSupervisor.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IQueueStorageRepository<Order> _queueStorage;
        private readonly ITableStorageRepository<Confirmation> _tableStorage;
        private readonly IMapper _mapper;

        public OrderRepository(IQueueStorageRepository<Order> queueStorage, ITableStorageRepository<Confirmation> tableStorage, IMapper mapper)
        {
            _queueStorage = queueStorage;
            _tableStorage = tableStorage;
            _mapper = mapper;
        }
        public async Task<OrderDTO> AddOrder(string orderText)
        {   
            var rand = new Random();
            var newOrder = await _queueStorage.Enqueue(
                new Order
                { RandomNumber = rand.Next(1, 10), OrderText = orderText });
            return _mapper.Map<OrderDTO>(newOrder);
        }

        public async Task<OrderDTO> GetOrder()
        {
            var order = await _queueStorage.Get();
            return order == null ? null : _mapper.Map<OrderDTO>(order);
        }

        public async Task RemoveOrder(OrderDTO order)
        {
            var orderToDelete = _mapper.Map<Order>(order);
            await _queueStorage.Dequeue(orderToDelete);
        }

        public async Task SaveOrder(OrderDTO order, Guid agentId)
        {
            var orderToSave = _mapper.Map<Order>(order);
            var confirmation = new Confirmation(orderToSave.OrderId, agentId);
            
            confirmation.AgentId = agentId;
            confirmation.OrderId = orderToSave.OrderId;
            confirmation.OrderStatus = "Processed";

            await _tableStorage.Save(confirmation);
        }
    }
}
