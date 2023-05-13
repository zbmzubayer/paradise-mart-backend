using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISellerRepo<TYPE>
    {
        bool UploadLogo(string guid, string photo);
    }
}
