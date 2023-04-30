using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ReviewRepo : Repo, IRepo<Review, int, bool>
    {
        public bool Create(Review obj)
        {
            db.Reviews.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Review> Get()
        {
            return db.Reviews.ToList();
        }
        public Review Get(int id)
        {
            return db.Reviews.Find(id);
        }
        public bool Update(Review obj)
        {
            var exReviews = Get(obj.Id);
            db.Entry(exReviews).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
        public bool Delete(int id)
        {
            db.Reviews.Remove(Get(id));
            return db.SaveChanges() > 0;
        }
    }
}
