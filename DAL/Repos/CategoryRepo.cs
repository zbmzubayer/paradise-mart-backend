using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CategoryRepo : Repo, IRepo<Category, int, bool>
    {
        public bool Create(Category obj)
        {
            db.Categories.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Category> Get()
        {
            return db.Categories.ToList();
        }
        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }
        public bool Update(Category obj)
        {
            var exCategory = Get(obj.Id);
            db.Entry(exCategory).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Categories.Remove(Get(id));
            return db.SaveChanges() > 0;
        }
    }
}
