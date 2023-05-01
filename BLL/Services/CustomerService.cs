using AutoMapper;
using BLL.DTOs;
using BLL.DTOs.Customer;
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
        /* Others */
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
        public static bool ChangePassword(string guid, ChangePassDTO changePasswordDTO)
        {
            var dbCustomer = DataAccessFactory.CustomerData().Get(guid);
            if(changePasswordDTO.CurrentPassword == dbCustomer.Password)
            {
                return DataAccessFactory.CustomerOthersData().ChangePassword(dbCustomer.Guid, changePasswordDTO.NewPassword);
            }
            return false;
        }
    }
}
