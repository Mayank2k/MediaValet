using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaValet.OrderSupervisor.Repository.Entities
{
    public class Confirmation: TableEntity
    {
        public Confirmation(int orderId, Guid agentId)
        {
            RowKey = orderId.ToString();
            PartitionKey = agentId.ToString();
        }
        public Guid AgentId { get; set; }
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }

    }
}
