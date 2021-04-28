using System;
using System.Collections.Generic;
using System.Text;

namespace MediaValet.OrderSupervisor.Repository.DTOs
{
    public class OrderDTO
    {
        public string QueueId { get; set; }        
        public string Receipt { get; set; }
        public int OrderId { get; set; }
        public int MagicNumber { get; set; }
        public string OrderText { get; set; }
    }
}
