using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.OTP
{
    public class OTPDTO
    {
        public int Id { get; set; }
        public string OtpCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        //public int AdminId { get; set; }
    }
}
