using BLL.DTOs.Review;
using BLL.DTOs.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Product
{
    public class ProductAllInfoDTO : ProductDTO
    {
        public SellerDTO Seller { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
    }
}
