using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitwiseCommerce.Orders.Contracts
{
    public interface IOrderAccepted
    {
        string Id { get; set; }
    }
}
