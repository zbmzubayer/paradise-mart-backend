using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepo<TYPE>
    {
        TYPE Get(int id);
        TYPE GetByEmail(string email);
        bool ChangePassword(string guid, string password);
    }
}
