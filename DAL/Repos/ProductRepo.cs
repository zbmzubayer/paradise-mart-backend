using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ProductRepo : Repo, IRepo<Product, string, bool>
    {
        public bool Create(Product obj)
        {
            db.Products.Add(obj);
            return db.SaveChanges() > 0;
        }
        public List<Product> Get()
        {
            return db.Products.ToList();
        }
        public Product Get(string url)
        {
            return db.Products.FirstOrDefault(p => p.Url == url);
        }
        public bool Update(Product obj)
        {
            var exProduct = Get(obj.Url);
            exProduct.Url = obj.Name.Replace(" ", "-") + "-" + exProduct.Guid;
            exProduct.Name = obj.Name;
            exProduct.Description = obj.Description;
            exProduct.Quantity = obj.Quantity;
            exProduct.Waranty = obj.Waranty;
            exProduct.Price = obj.Price;
            exProduct.CategoryId = obj.CategoryId;
            return db.SaveChanges() > 0;
        }
        public bool Delete(string url)
        {
            db.Products.Remove(Get(url));
            return db.SaveChanges() > 0;
        }
    }
}
