using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class PaymentRepo : Repo, IRepo<Payment, int, bool>
    {
        public bool Create(Payment obj)
        {
            db.Payments.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Payment> Get()
        {
            return db.Payments.ToList();
        }
        public Payment Get(int id)
        {
            return db.Payments.Find(id);
        }
        public bool Update(Payment obj)
        {
            var exPayment = Get(obj.Id);
            db.Entry(exPayment).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Payments.Remove(Get(id));
            return db.SaveChanges() > 0;
        }
    }
}
