using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderTrackingManager
    {
        void Add(OrderTracking orderTracking);
        void Remove(OrderTracking orderTracking);
        void Update(OrderTracking orderTracking);
        OrderTracking GetOrderTrackingById(int id);
    }
}
