using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Type { get; set; }
        // Payment (1) <---> (*) CustomerPayment
        public virtual ICollection<CustomerPayment> CustomerPayments { get; set; }
        public Payment()
        {
            CustomerPayments = new List<CustomerPayment>();
        }
    }
}
