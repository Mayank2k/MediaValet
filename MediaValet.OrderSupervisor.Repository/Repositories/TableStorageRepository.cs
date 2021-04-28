using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MediaValet.OrderSupervisor.Repository.Repositories
{
    public class TableStorageRepository<T> : ITableStorageRepository<T> where T : TableEntity
    {
        private readonly CloudTable _table;
        private string tableName = "confirmations";
        public TableStorageRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudTableClient();

            var table = client.GetTableReference(tableName);
            table.CreateIfNotExists();

            _table = table;
        }

        public async Task Save(T entity)
        {            
            TableOperation insertOperation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
