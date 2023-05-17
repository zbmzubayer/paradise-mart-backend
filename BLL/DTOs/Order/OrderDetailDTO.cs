using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.DTOs.Product;

namespace BLL.DTOs.Order
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}
