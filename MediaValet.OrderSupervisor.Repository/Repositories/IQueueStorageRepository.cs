using MediaValet.OrderSupervisor.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.Repository.Repositories
{
    public interface IQueueStorageRepository<T> where T : QueueEntity
    {
        Task<T> Get();
        Task<T> Enqueue(T entity);
        Task Dequeue(T entity);
    }
}
