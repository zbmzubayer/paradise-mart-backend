using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class OrderDetailRepo : Repo, IRepo<List<OrderDetail>, int, bool>
    {
        public bool Create(List<OrderDetail> obj)
        {
            foreach(var item in obj)
            {
                db.OrderDetails.Add(item);
            }
            //return db.SaveChanges() > 0;
            return true;
        }
        public bool Delete(int id)
        {
            var orderDetails = (from od in db.OrderDetails where od.OrderId == id select od).ToList();
            foreach(var items in orderDetails)
            {
                db.OrderDetails.Remove(items);
            }
            return db.SaveChanges() > 0;
        }
        public List<List<OrderDetail>> Get()
        {
            throw new NotImplementedException();
        }
        public List<OrderDetail> Get(int id)
        {
            throw new NotImplementedException();
        }
        public bool Update(List<OrderDetail> obj)
        {
            throw new NotImplementedException();
        }
    }
}
