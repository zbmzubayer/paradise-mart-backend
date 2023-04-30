using AutoMapper;
using BLL.DTOs.Category;
using BLL.DTOs.Product;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService
    {
        public static bool Create(CategoryDTO category)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CategoryDTO, Category>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Category>(category);
            return DataAccessFactory.CategoryData().Create(mapped);
        }
        public static List<CategoryDTO> Get()
        {
            var data = DataAccessFactory.CategoryData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<CategoryDTO>>(data);
        }
        public static CategoryDTO Get(int id)
        {
            var data = DataAccessFactory.CategoryData().Get(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CategoryDTO>(data);
        }
        public static bool Update(CategoryDTO category)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CategoryDTO, Category>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Category>(category);
            return DataAccessFactory.CategoryData().Update(mapped);
        }
        public static bool Delete(int id)
        {
            return DataAccessFactory.CategoryData().Delete(id);
        }
        // Category + Products
        public static CategoryProductsDTO GetWithProducts(int id)
        {
            var data = DataAccessFactory.CategoryData().Get(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Category, CategoryProductsDTO>();
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CategoryProductsDTO>(data);
        }
    }
}
