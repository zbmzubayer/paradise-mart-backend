using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Review
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
    }
}
