using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
    public interface IThreedProcessor : IPaymentProcessor<MpiRequest>
    {
        PaymentResponse CompleteThreed(VposRequest input);
    }
}
