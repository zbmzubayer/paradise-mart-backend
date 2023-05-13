using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepo<TYPE>
    {
        bool ProcessOrder(string code, string status);
        bool DeliverOrder(string code, string status, DateTime deliveredAt);
    }
}
