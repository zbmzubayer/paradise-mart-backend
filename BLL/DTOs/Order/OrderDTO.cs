using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public double Amount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public int  CustomerId{ get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
