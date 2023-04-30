using AutoMapper;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Order;

namespace BLL.Services
{
    public class OrderService
    {
        public static Order Create(OrderDTO order, List<OrderDetailDTO> orderDetails)
        {
            var cfg = new MapperConfiguration(c =>
            {
                c.CreateMap<OrderDTO, Order>();
            });
            var mapper = new Mapper(cfg);
            var mapped = mapper.Map<Order>(order);
            mapped.Amount = 0;
            mapped.PlacedAt = DateTime.Now;
            var dbOrder = DataAccessFactory.OrderData().Create(mapped);
            double orderSum = 0;
            foreach (var item in orderDetails)
            {
                item.OrderId = dbOrder.Id;
                orderSum += item.Price;
            }
            return mapped;
            // Incomplete
        }
        
    }
}
