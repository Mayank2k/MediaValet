using MediaValet.OrderSupervisor.Repository.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.Repository.Repositories
{
    public interface IOrderRepository
    {
        Task<OrderDTO> AddOrder(string orderText);
        Task RemoveOrder(OrderDTO order);
        Task<OrderDTO> GetOrder();
        Task SaveOrder (OrderDTO order, Guid agentId);
    }
}
