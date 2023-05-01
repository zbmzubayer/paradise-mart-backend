using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class AdminRepo : Repo, IRepo<Admin, string, bool>, IUserRepo<Admin>
    {
        public bool Create(Admin obj)
        {
            db.Admins.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Admin> Get()
        {
            return db.Admins.ToList();
        }
        public Admin Get(string guid)
        {
            return db.Admins.FirstOrDefault(a => a.Guid == guid);
        }
        public bool Update(Admin obj)
        {
            var exAdmin = Get(obj.Guid);
            exAdmin.Name = obj.Name;
            exAdmin.Email = obj.Email;
            exAdmin.Phone = obj.Phone;
            exAdmin.Gender = obj.Gender;
            exAdmin.Dob = obj.Dob;
            exAdmin.Address = obj.Address;
            return db.SaveChanges() > 0;
        }
        public bool Delete(string guid)
        {
            db.Admins.Remove(Get(guid));
            return db.SaveChanges() > 0;
        }
        // Others
        public Admin Get(int id)
        {
            return db.Admins.Find(id);
        }
        public Admin GetByEmail(string email)
        {
            return db.Admins.FirstOrDefault(a => a.Email == email);
        }
        public bool UploadPhoto(string guid, string photo)
        {
            var dbAdmin = Get(guid);
            dbAdmin.Photo = photo;
            return db.SaveChanges() > 0;
        }
        public bool ChangePassword(string guid, string password)
        {
            var admin = Get(guid);
            admin.Password = password;
            return db.SaveChanges() > 0;
        }

        
    }
}
