using BLL.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Customer
{
    public class CustomerOrdersDTO : CustomerDTO
    {
        public List<OrderDTO> Orders { get; set; }
        public CustomerOrdersDTO()
        {
            Orders = new List<OrderDTO>();
        }
    }
}
