using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class SellerRepo : Repo, IRepo<Seller, string, bool>, IUserRepo<Seller>
    {
        public bool Create(Seller obj)
        {
            db.Sellers.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Seller> Get()
        {
            return db.Sellers.ToList();
        }
        public Seller Get(string guid)
        {
            return db.Sellers.FirstOrDefault(s => s.Guid == guid);
        }
        public bool Update(Seller obj)
        {
            var exSeller = Get(obj.Guid);
            exSeller.Name = obj.Name;
            exSeller.Email = obj.Email;
            exSeller.Phone = obj.Phone;
            exSeller.Dob = obj.Dob;
            exSeller.Gender = obj.Gender;
            exSeller.Address = obj.Address;
            exSeller.CompanyName = obj.CompanyName;
            exSeller.CompanyLogo = obj.CompanyLogo;
            return db.SaveChanges() > 0;
        }
        public bool Delete(string guid)
        {
            db.Sellers.Remove(Get(guid));
            return db.SaveChanges() > 0;
        }
        // Others
        public Seller Get(int id)
        {
            return db.Sellers.Find(id);
        }
        public Seller GetByEmail(string email)
        {
            return db.Sellers.FirstOrDefault(s => s.Email == email);
        }
        public bool UploadPhoto(string guid, string photo)
        {
            var dbSeller = Get(guid);
            dbSeller.Photo = photo;
            return db.SaveChanges() > 0;
        }
        public bool ChangePassword(string guid, string password)
        {
            var seller = Get(guid);
            seller.Password = password;
            return db.SaveChanges() > 0;
        }
    }
}
