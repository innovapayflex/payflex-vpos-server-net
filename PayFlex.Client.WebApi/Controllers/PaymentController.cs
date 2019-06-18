using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace PayFlex.Client.WebApi.Controllers
{
    public class PaymentController : ApiController
    {
        [HttpPost]
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<PaymentResponse> Pay(VposRequest value)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            try
            {
                PaymentManager paymentManager = new PaymentManager();
                value.PaymentType = PaymentType.VPos;
                value.TransactionId = Guid.NewGuid().ToString();
                value.OrderId = Guid.NewGuid().ToString();
                PaymentResponse paymentResponse = paymentManager.PostProcess(value);

                return paymentResponse;
            }
            catch (Exception ex)
            {
                return new PaymentResponse()
                {
                    Response = ex.Message,
                    IsSuccessful = false
                };
            }
        }
    }
}
