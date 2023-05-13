using AutoMapper;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.CustomerPayment;

namespace BLL.Services
{
    public class CustomerPaymentService
    {
        public static bool Create(CustomerPaymentDTO customerPayment)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerPaymentDTO, CustomerPayment>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CustomerPayment>(customerPayment);
            mapped.ExpiredAt = DateTime.UtcNow.AddDays(60);
            return DataAccessFactory.CustomerPaymentData().Create(mapped);
        }
        public static List<CustomerPaymentDTO> Get()
        {
            var data = DataAccessFactory.CustomerPaymentData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerPayment, CustomerPaymentDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<CustomerPaymentDTO>>(data);
        }
        public static CustomerPaymentDTO Get(int id)
        {
            var data = DataAccessFactory.CustomerPaymentData().Get(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerPayment, CustomerPaymentDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<CustomerPaymentDTO>(data);
        }
        public static bool Update(CustomerPaymentDTO customerPayment)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<CustomerPaymentDTO, CustomerPayment>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<CustomerPayment>(customerPayment);
            mapped.ExpiredAt = DateTime.UtcNow.AddDays(60);
            return DataAccessFactory.CustomerPaymentData().Update(mapped);
        }
        public static bool Delete(int id)
        {
            return DataAccessFactory.CustomerPaymentData().Delete(id);
        }
    }
}
