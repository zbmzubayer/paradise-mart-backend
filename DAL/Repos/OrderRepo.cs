using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class OrderRepo : Repo, IRepo<Order, string, Order>
    {
        public Order Create(Order obj)
        {
            var order = db.Orders.Add(obj);
            db.SaveChanges();
            return order;
        }
        public List<Order> Get()
        {
            throw new NotImplementedException();
        }
        public Order Get(string id)
        {
            throw new NotImplementedException();
        }
        public Order Update(Order obj)
        {
            throw new NotImplementedException();
        }
        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }
    }
}
