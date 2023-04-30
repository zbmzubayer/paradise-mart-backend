using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductPhoto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Photo { get; set; }
        // Product (1) <---> (*) ProductPhoto
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
