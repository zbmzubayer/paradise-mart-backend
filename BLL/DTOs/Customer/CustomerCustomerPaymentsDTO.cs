using BLL.DTOs.CustomerPayment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Customer
{
    public class CustomerCustomerPaymentsDTO : CustomerDTO
    {
        public List<CustomerPaymentDTO> CustomerPayments { get; set; }
        public CustomerCustomerPaymentsDTO()
        {
            CustomerPayments = new List<CustomerPaymentDTO>();
        }
    }
}
