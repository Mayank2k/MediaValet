using System;
using System.Collections.Generic;
using System.Text;

namespace MediaValet.OrderSupervisor.Repository.Entities
{
    public abstract class QueueEntity
    {
        public string QueueId { get; set; }
        public string Receipt { get; set; }
        public abstract void SetEntityId(int id);
    }
}
