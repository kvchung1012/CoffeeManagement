using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application
{
    public interface IPaymentGatewayService
    {
        public Task<string> GetPaymentUrl(long orderId);
    }
}
