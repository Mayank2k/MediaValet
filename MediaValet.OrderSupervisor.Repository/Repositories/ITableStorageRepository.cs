using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.Repository.Repositories
{
    public interface ITableStorageRepository<T> where T : TableEntity
    {
        Task Save(T entity);
    }
}
