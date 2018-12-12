using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client.Processor
{
    public interface IVposPaymentProcessor : IPaymentProcessor<VposRequest>
    {
        PaymentResponse SettlementQuery(SettlementRequest input);
        PaymentResponse SettlementDetailQuery(SettlementDetailRequest input);
        PaymentResponse Search(VposSearchRequest input);
        PaymentResponse SucceededOpenBatchTransactions(OpenBatchTransactionsRequest input);
    }
}
