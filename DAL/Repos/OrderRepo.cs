using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class OrderRepo : Repo, IRepo<Order, string, Order>, IOrderRepo<Order>
    {
        public Order Create(Order obj)
        {
            var order = db.Orders.Add(obj);
            db.SaveChanges();
            return order;
        }
        public List<Order> Get()
        {
            return db.Orders.ToList();
        }
        public Order Get(string code)
        {
            return db.Orders.FirstOrDefault(o => o.Code == code);
        }
        public Order Update(Order obj)
        {
            var dbOrder = Get(obj.Code);
            dbOrder.Amount = obj.Amount;
            db.SaveChanges();
            return dbOrder;
        }
        public bool Delete(string code)
        {
            db.Orders.Remove(Get(code));
            return db.SaveChanges() > 0;
        }
        public bool ProcessOrder(string code, string status)
        {
            var dbOrder = Get(code);
            dbOrder.Status = status;
            return db.SaveChanges() > 0;
        }
        public bool DeliverOrder(string code, string status, DateTime deliveredAt)
        {
            var dbOrder = Get(code);
            dbOrder.Status = status;
            dbOrder.DeliveredAt = deliveredAt;
            return db.SaveChanges() > 0;
        }
    }
}
