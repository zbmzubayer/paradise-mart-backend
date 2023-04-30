using BLL.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Category
{
    public class CategoryProductsDTO : CategoryDTO
    {
        public List<ProductDTO> Products { get; set; }
    }
}
