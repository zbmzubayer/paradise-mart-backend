using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Customer;
using BLL.DTOs.CustomerPayment;
using BLL.DTOs.Order;
using BLL.DTOs.Review;
using BLL.Helpers;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Services
{
    public class CustomerService
    {
        public static int iteration = 4;
        public static bool Create(CustomerCreateDTO customer)
        {
            // MailService.CustomerRegistration(customer.Name, customer.Email);

            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerCreateDTO, Customer>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Customer>(customer);
            mapped.Guid = Guid.NewGuid().ToString("N");
            mapped.Salt = PasswordHash.GenerateSalt();
            mapped.Password = PasswordHash.GenerateHash(mapped.Password, mapped.Salt, iteration);
            mapped.CreatedAt = DateTime.Now;
            return DataAccessFactory.CustomerData().Create(mapped);
        }
        public static List<CustomerDTO> Get()
        {
            var data = DataAccessFactory.CustomerData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<CustomerDTO>>(data);
        }
        public static CustomerDTO Get(string guid)
        {
            var data = DataAccessFactory.CustomerData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerDTO>(data);
        }
        public static bool Update(CustomerDTO customer)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerDTO, Customer>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Customer>(customer);
            mapped.ModifiedAt = DateTime.Now;
            return DataAccessFactory.CustomerData().Update(mapped);
        }
        public static bool Delete(string guid)
        {
            return DataAccessFactory.CustomerData().Delete(guid);
        }
        // Customer + Orders
        public static CustomerOrdersDTO GetWithOrders(string guid)
        {
            var data = DataAccessFactory.CustomerData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerOrdersDTO>();
                c.CreateMap<Order, OrderDTO>();
                c.CreateMap<OrderDetail, OrderDetailDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerOrdersDTO>(data);
        }
        // Customer + Reviews
        public static CustomerReviewsDTO GetWithReviews(string guid)
        {
            var data = DataAccessFactory.CustomerData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerReviewsDTO>();
                c.CreateMap<Review, ReviewDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerReviewsDTO>(data);
        }
        // Customer + CustomerPayments
        public static CustomerCustomerPaymentsDTO GetWithCustomerPayments(string guid)
        {
            var data = DataAccessFactory.CustomerData().Get(guid);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerCustomerPaymentsDTO>();
                c.CreateMap<CustomerPayment, CustomerPaymentDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerCustomerPaymentsDTO>(data);
        }
        // Others
        public static CustomerDTO Get(int id)
        {
            var data = DataAccessFactory.CustomerOthersData().Get(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerDTO>(data);
        }
        public static CustomerDTO GetByEmail(string email)
        {
            var data = DataAccessFactory.CustomerOthersData().GetByEmail(email);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Customer, CustomerDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerDTO>(data);
        }
        public static bool UploadPhoto(string guid, string photo)
        {
            return DataAccessFactory.CustomerOthersData().UploadPhoto(guid, photo);
        }
        public static bool DeletePhoto(string guid)
        {
            return DataAccessFactory.CustomerOthersData().DeletePhoto(guid);
        }
        public static bool ChangePassword(string guid, ChangePassDTO changePasswordDTO)
        {
            var dbUser = DataAccessFactory.CustomerData().Get(guid);
            changePasswordDTO.CurrentPassword = PasswordHash.GenerateHash(changePasswordDTO.CurrentPassword, dbUser.Salt, iteration);
            if(changePasswordDTO.CurrentPassword == dbUser.Password)
            {
                changePasswordDTO.NewPassword = PasswordHash.GenerateHash(changePasswordDTO.NewPassword, dbUser.Salt, iteration);
                return DataAccessFactory.CustomerOthersData().ChangePassword(dbUser.Guid, changePasswordDTO.NewPassword);
            }
            return false;
        }
        public static bool ChangeEmail(string guid, ChangeEmailDTO changeEmailDTO)
        {
            var dbUser = DataAccessFactory.CustomerData().Get(guid);
            changeEmailDTO.Password = PasswordHash.GenerateHash(changeEmailDTO.Password, dbUser.Salt, iteration);
            if (changeEmailDTO.Password == dbUser.Password)
            {
                return DataAccessFactory.CustomerOthersData().ChangeEmail(guid, changeEmailDTO.Email);
            }
            return false;
        }
    }
}
