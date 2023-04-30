using AutoMapper;
using BLL.DTOs.Payment;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PaymentService
    {
        public static bool Create(PaymentDTO payment)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<PaymentDTO, Payment>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Payment>(payment);
            return DataAccessFactory.PaymentData().Create(mapped);
        }
        public static List<PaymentDTO> Get()
        {
            var data = DataAccessFactory.PaymentData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Payment, PaymentDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<PaymentDTO>>(data);
        }
        public static PaymentDTO Get(int id)
        {
            var data = DataAccessFactory.PaymentData().Get(id);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Payment, PaymentDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<PaymentDTO>(data);
        }
        public static bool Update(PaymentDTO payment)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<PaymentDTO, Payment>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Payment>(payment);
            return DataAccessFactory.PaymentData().Update(mapped);
        }
        public static bool Delete(int id)
        {
            return DataAccessFactory.PaymentData().Delete(id);
        }
    }
}
