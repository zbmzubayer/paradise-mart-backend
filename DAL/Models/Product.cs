using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Guid { get; set; }
        [Required]
        [StringLength(150)]
        public string Url { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [StringLength(20)]
        public string Waranty { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        // Product (1) <---> (*) ProductPhoto
        public virtual ICollection<ProductPhoto> ProductPhotos { get; set; }
        // Category (1) <---> (*) Product
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        // Seller (1) <---> (*) Product
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        public virtual Seller Seller { get; set; }
        // Product (1) <---> (*) OrderDetail
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        // Product (1) <---> (*) Review
        public virtual ICollection<Review> Reviews { get; set; }
        public Product()
        {
            ProductPhotos = new List<ProductPhoto>();
            OrderDetails = new List<OrderDetail>();
            Reviews = new List<Review>();
        }
    }
}
