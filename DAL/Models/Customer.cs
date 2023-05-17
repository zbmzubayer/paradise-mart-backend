using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Guid { get; set; }
        [StringLength(50)]
        public string Photo { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength (20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
        [Required]
        public DateTime Dob { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Salt { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        // Customer (1) <---> (*) Order
        public virtual ICollection<Order> Orders { get; set; }
        // Customer (1) <---> (*) Review
        public virtual ICollection<Review> Reviews { get; set; }
        // Customer (1) <---> (*) CustomerPayment
        public virtual ICollection<CustomerPayment> CustomerPayments { get; set; }
        // Customer (1) <---> (*) OTP
        public virtual ICollection<OneTimePassword> OneTimePasswords { get; set; }
        public Customer()
        {
            Orders = new List<Order>();
            Reviews = new List<Review>();
            CustomerPayments = new List<CustomerPayment>();
            OneTimePasswords = new List<OneTimePassword>();
        }
    }
}
