using MediaValet.OrderSupervisor.Repository.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Queues;
using Newtonsoft.Json;

namespace MediaValet.OrderSupervisor.Repository.Repositories
{
    public class QueueStorageRepository<T> : IQueueStorageRepository<T> where T : QueueEntity
    {
        private string queueName = "orders";
        private int _id;
        private readonly QueueClient _queueClient;

        public QueueStorageRepository(IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var queueClient = new QueueClient(connectionString, queueName);
            queueClient.CreateIfNotExists();

            _queueClient = queueClient;
        }
        public async Task Dequeue(T entity)
        {
            await _queueClient.DeleteMessageAsync(entity.QueueId, entity.Receipt);
        }

        public async Task<T> Enqueue(T entity)
        {
            _id = _id + 1;
            entity.SetEntityId(_id);
            await _queueClient.SendMessageAsync(JsonConvert.SerializeObject(entity));

            return entity;
        }

        public async Task<T> Get()
        {
            var message = await _queueClient.ReceiveMessageAsync();
            if (message.Value == null) return null;

            var entity = JsonConvert.DeserializeObject<T>(message.Value.MessageText);
            entity.QueueId = message.Value.MessageId;
            entity.Receipt = message.Value.PopReceipt;

            return entity;

        }
    }
}
