using System;
using System.Collections.Generic;
using System.Text;

namespace MediaValet.OrderSupervisor.Repository.Entities
{
    public class Order : QueueEntity
    {
        public int OrderId { get; set; }
        public int RandomNumber { get; set; }
        public string OrderText { get; set; }

        public override void SetEntityId(int id)
        {
            OrderId = id;
        }

    }
}
