using AutoMapper;
using BLL.DTOs.Product;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Customer;
using BLL.DTOs.Seller;

namespace BLL.Services
{
    public class ProductService
    {
        public static bool Create(ProductDTO product)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ProductDTO, Product>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Product>(product);
            mapped.Guid = Guid.NewGuid().ToString("N");
            mapped.Url = mapped.Name.Replace(" ", "-") + "-" + mapped.Guid;
            mapped.CreatedAt = DateTime.Now;
            return DataAccessFactory.ProductData().Create(mapped);
        }
        public static List<ProductDTO> Get()
        {
            var data = DataAccessFactory.ProductData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<ProductDTO>>(data);
        }
        public static ProductDTO Get(string url)
        {
            var data = DataAccessFactory.ProductData().Get(url);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<ProductDTO>(data);
        }
        /*        public static ProductDTO Get(int id)
                {
                    var data = DataAccessFactory.ProductData().Get(id);
                    var cfg = new MapperConfiguration(c =>
                    {
                        c.CreateMap<Product, ProductDTO>();
                    });
                    var mapper = new Mapper(cfg);
                    return mapper.Map<ProductDTO>(data);
                }*/
        public static bool Update(ProductDTO productUpdate)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<ProductDTO, Product>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Product>(productUpdate);
            mapped.ModifiedAt = DateTime.Now;
            return DataAccessFactory.ProductData().Update(mapped);
        }
        public static bool Delete(string url)
        {
            return DataAccessFactory.ProductData().Delete(url);
        }
        // Others
        
    }
}
