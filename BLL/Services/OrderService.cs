using AutoMapper;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Order;
using BLL.DTOs.Category;
using BLL.DTOs.Customer;

namespace BLL.Services
{
    public class OrderService
    {
        public static bool Create(OrderDTO order)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderDTO, Order>();
                c.CreateMap<OrderDetailDTO, OrderDetail>();
            });
            var mapper = new Mapper(cfg);
            var mapped1 = mapper.Map<Order>(order);
            var mapped2 = mapper.Map<List<OrderDetail>>(order.OrderDetails);
            mapped1.Code = "O" +  mapped1.CustomerId + DateTimeOffset.Now.ToUnixTimeMilliseconds();
            mapped1.Status = "Pending";
            mapped1.Amount = 0;
            mapped1.PlacedAt = DateTime.Now;
            var dbOrder = DataAccessFactory.OrderData().Create(mapped1);
            foreach (var item in mapped2)
            {
                item.OrderId = dbOrder.Id;
                dbOrder.Amount += item.Price * item.Quantity;
            }
            DataAccessFactory.OrderDetailData().Create(mapped2);
            DataAccessFactory.OrderData().Update(dbOrder);
            if (dbOrder != null) return true;
            return false;
        }
        public static List<OrderDTO> Get()
        {
            var data = DataAccessFactory.OrderData().Get();
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Order, OrderDTO>();
                c.CreateMap<OrderDetail, OrderDetailDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<List<OrderDTO>>(data);
        }
        public static OrderDTO Get(string code)
        {
            var data = DataAccessFactory.OrderData().Get(code);
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<Order, OrderDTO>();
                c.CreateMap<OrderDetail, OrderDetailDTO>();
            });
            var mapper = new Mapper(cfg);
            return mapper.Map<OrderDTO>(data);
        }
        public static bool Delete(string code)
        {
            var dbOrder = Get(code);
            DataAccessFactory.OrderDetailData().Delete(dbOrder.Id);
            return DataAccessFactory.OrderData().Delete(code);
        }
        public static bool ProcessOrder(string guid)
        {
            string status = "Processing";
            return DataAccessFactory.OrderIndividualData().ProcessOrder(guid, status);
        }
        public static bool DeliverOrder(string guid)
        {
            string status = "Delivered";
            DateTime deliveredAt = DateTime.Now;
            return DataAccessFactory.OrderIndividualData().DeliverOrder(guid, status, deliveredAt);
        }
    }
}
