using BLL.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Seller
{
    public class SellerProductsDTO : SellerDTO
    {
        public List<ProductDTO> Products { get; set; }
        /*public SellerProductsDTO()
        {
            Products = new List<ProductDTO>();
        }*/
    }
}
