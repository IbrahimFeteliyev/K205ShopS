using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderManager
    {
        void Add(Order order);
        void Remove(Order order);
        void Update(Order order);
        List<Order> GetAll(int userId);
    }
}
