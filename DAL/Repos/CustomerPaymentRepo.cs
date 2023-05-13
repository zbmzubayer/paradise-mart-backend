using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CustomerPaymentRepo : Repo, IRepo<CustomerPayment, int, bool>
    {
        public bool Create(CustomerPayment obj)
        {
            db.CustomerPayments.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<CustomerPayment> Get()
        {
            return db.CustomerPayments.ToList();
        }
        public CustomerPayment Get(int id)
        {
            return db.CustomerPayments.Find(id);
        }
        public bool Update(CustomerPayment obj)
        {
            var exCustomerPayment = Get(obj.Id);
            exCustomerPayment.AccountNo = obj.AccountNo;
            exCustomerPayment.ExpiredAt = obj.ExpiredAt;
            exCustomerPayment.PaymentId = obj.PaymentId;
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.CustomerPayments.Remove(Get(id));
            return db.SaveChanges() > 0;
        }
    }
}
