using BLL.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Customer
{
    public class CustomerReviewsDTO : CustomerDTO
    {
        public List<ReviewDTO> Reviews { get; set; }
        public CustomerReviewsDTO()
        {
           Reviews  = new List<ReviewDTO>();
        }
    }
}