using Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int K205UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalQuantity { get; set; }
        public OrderTracking OrderTracking { get; set; }
        public int OrderTrackingId { get; set; }
        public bool IsDelivered { get; set; }
    }
}
