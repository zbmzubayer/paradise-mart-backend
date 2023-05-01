using AutoMapper;
using BLL.DTOs.Seller;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Customer;
using BLL.DTOs.Product;

namespace BLL.Services
{
    public class SellerService
    {
        public static bool Create(SellerCreateDTO seller)
        {
            // MailService.SellerRegistration(seller.Name, seller.Email);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<SellerCreateDTO, Seller>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Seller>(seller);
            mapped.Guid = Guid.NewGuid().ToString("N");
            mapped.CreatedAt = DateTime.Now;
            mapped.Status = "New";
            return DataAccessFactory.SellerData().Create(mapped);
        }
        public static List<SellerDTO> Get()
        {
            var data = DataAccessFactory.SellerData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Seller, SellerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<SellerDTO>>(data);
        }
        public static SellerDTO Get(string guid)
        {
            var data = DataAccessFactory.SellerData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Seller, SellerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<SellerDTO>(data);
        }
        public static bool Update(SellerDTO seller)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<SellerDTO, Seller>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Seller>(seller);
            mapped.ModifiedAt = DateTime.Now;
            return DataAccessFactory.SellerData().Update(mapped);
        }
        public static bool Delete(string guid)
        {
            return DataAccessFactory.SellerData().Delete(guid);
        }
        // Others
        public static SellerDTO GetByEmail(string email)
        {
            var data = DataAccessFactory.SellerOthersData().GetByEmail(email);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Seller, SellerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<SellerDTO>(data);
        }
        public static bool UploadPhoto(string guid, string photo)
        {
            return DataAccessFactory.SellerOthersData().UploadPhoto(guid, photo);
        }
        public static bool DeletePhoto(string guid)
        {
            return DataAccessFactory.SellerOthersData().DeletePhoto(guid);
        }
        public static bool ChangePassword(string guid, ChangePassDTO changePasswordDTO)
        {
            var dbSeller = DataAccessFactory.SellerData().Get(guid);
            if (changePasswordDTO.CurrentPassword == dbSeller.Password)
            {
                return DataAccessFactory.SellerOthersData().ChangePassword(dbSeller.Guid, changePasswordDTO.NewPassword);
            }
            return false;
        }
        // Seller + Products
        public static SellerProductsDTO GetWithProducts(string guid)
        {
            var data = DataAccessFactory.SellerData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Seller, SellerProductsDTO>();
                c.CreateMap<Product, ProductDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<SellerProductsDTO>(data);
        }
    }
}
