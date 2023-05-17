using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OneTimePassword
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string OtpCode { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime ExpiredAt { get; set; }
        // Customer (1) <---> (*) OTP
        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        /*// Seller (1) <---> (*) OTP
        [ForeignKey("Seller")]
        public int? SellerId { get; set; }
        public virtual Seller Seller { get; set; }*/

        /*// Admin (1) <---> (*) OTP
        [ForeignKey("Admin")]
        public int? AdminId { get; set; }
        public virtual Admin Admin { get; set; }*/
    }
}
