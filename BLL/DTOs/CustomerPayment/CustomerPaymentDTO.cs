using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.CustomerPayment
{
    public class CustomerPaymentDTO
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public DateTime ExpiredAt { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
    }
}
