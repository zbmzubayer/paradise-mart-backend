using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class OTPRepo : Repo, IRepo<OneTimePassword, int, bool>, IOTPRepo<OneTimePassword>
    {
        public bool Create(OneTimePassword obj)
        {
            db.OneTimePasswords.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<OneTimePassword> Get()
        {
            return db.OneTimePasswords.ToList();
        }
        public OneTimePassword Get(int id)
        {
            return db.OneTimePasswords.Find(id);
        }

        public bool Update(OneTimePassword obj)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public OneTimePassword Verify(string code, DateTime date)
        {
            return db.OneTimePasswords.SingleOrDefault(o => o.OtpCode == code && o.ExpiredAt >= date);
        }
    }
}
