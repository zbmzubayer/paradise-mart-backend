using AutoMapper;
using BLL.DTOs.Admin;
using BLL.DTOs.Customer;
using BLL.DTOs.Seller;
using BLL.Helpers;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService
    {
        public static CustomerDTO AuthenticateCustomer(string email, string password)
        {
            var dbUser = DataAccessFactory.CustomerOthersData().GetByEmail(email);
            if(dbUser != null)
            {
                var hashedPass = PasswordHash.GenerateHash(password, dbUser.Salt, CustomerService.iteration);
                var data = DataAccessFactory.CustomerAuthData().AuthenticateUser(email, hashedPass);
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Customer, CustomerDTO>();
                });
                var mapper = new Mapper(cfg);
                return mapper.Map<CustomerDTO>(data);
            }
            return null;
        }
        public static SellerDTO AuthenticateSeller(string email, string password)
        {
            var dbUser = DataAccessFactory.SellerOthersData().GetByEmail(email);
            if (dbUser != null)
            {
                var hashedPass = PasswordHash.GenerateHash(password, dbUser.Salt, SellerService.iteration);
                var data = DataAccessFactory.SellerAuthData().AuthenticateUser(email, hashedPass);
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Seller, SellerDTO>();
                });
                var mapper = new Mapper(cfg);
                return mapper.Map<SellerDTO>(data);
            }
            return null;
        }
        public static AdminDTO AuthenticateAdmin(string email, string password)
        {
            var dbUser = DataAccessFactory.AdminOthersData().GetByEmail(email);
            if (dbUser != null)
            {
                var hashedPass = PasswordHash.GenerateHash(password, dbUser.Salt, AdminService.iteration);
                var data = DataAccessFactory.AdminAuthData().AuthenticateUser(email, hashedPass);
                var cfg = new MapperConfiguration(c =>
                {
                    c.CreateMap<Admin, AdminDTO>();
                });
                var mapper = new Mapper(cfg);
                return mapper.Map<AdminDTO>(data);
            }
            return null;
        }
    }
}
