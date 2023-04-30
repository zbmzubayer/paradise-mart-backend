using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CustomerRepo : Repo, IRepo<Customer, string, bool>, IUserRepo<Customer>
    {
        public bool Create(Customer obj)
        {
            db.Customers.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Customer> Get()
        {
            return db.Customers.ToList();
        }
        public Customer Get(string guid)
        {
            return db.Customers.FirstOrDefault(c => c.Guid == guid);
        }
        
        public bool Update(Customer obj)
        {
            var exCustomer = Get(obj.Id);
            exCustomer.Photo = obj.Photo;
            exCustomer.Name = obj.Name;
            exCustomer.Email = obj.Email;
            exCustomer.Phone = obj.Phone;
            exCustomer.Gender = obj.Gender;
            exCustomer.Dob = obj.Dob;
            exCustomer.Address = obj.Address;

            return db.SaveChanges() > 0;
        }
        public bool Delete(string guid)
        {
            db.Customers.Remove(Get(guid));
            return db.SaveChanges() > 0;
        }
        // Others
        public Customer Get(int id)
        {
            return db.Customers.Find(id);
        }
        public Customer GetByEmail(string email)
        {
            var customer = (from c in db.Customers where c.Email.Equals(email) select c).FirstOrDefault();
            return customer;
        }
        public bool ChangePassword(string guid, string password)
        {
            var customer = Get(guid);
            customer.Password = password;
            return db.SaveChanges() > 0;
        }
    }
}
