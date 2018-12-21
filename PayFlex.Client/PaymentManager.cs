using PayFlex.Client.Processor;

namespace PayFlex.Client
{
    public class PaymentManager
    {
        public PaymentManager()
        {
            PaymentProcessorFactory<IVposPaymentProcessor>.Register(VposPaymentSupplier.VPos, () => new PayFlexVPosProcessor());
            PaymentProcessorFactory<IThreedProcessor>.Register(VposPaymentSupplier.MPI, () => new PayFlexMPIProcessor());
            PaymentProcessorFactory<ICommonPaymentProcessor>.Register(VposPaymentSupplier.CommonPayment, () => new PayFlexCommonPaymentProcessor());
        }

        public PaymentResponse PostProcess(Payment payment)
        {
            try
            {
                PaymentResponse result = null;

                switch (payment.PaymentType)
                {
                    case PaymentType.VPos:

                        var _paymentVposProcessor = PaymentProcessorFactory<IVposPaymentProcessor>.Create(VposPaymentSupplier.VPos);
                        VposRequest vposRequest = payment as VposRequest;
                        result = _paymentVposProcessor.Pay(vposRequest);

                        break;
                    case PaymentType.MPI:

                        var _paymentMpiProcessor = PaymentProcessorFactory<IThreedProcessor>.Create(VposPaymentSupplier.MPI);
                        MpiRequest mpiRequest = payment as MpiRequest;
                        result = _paymentMpiProcessor.Pay(mpiRequest);

                        break;
                    case PaymentType.CommonPayment:

                        var _paymentCommonProcessor = PaymentProcessorFactory<ICommonPaymentProcessor>.Create(VposPaymentSupplier.CommonPayment);
                        CommonPaymentRequest commonPaymentRequest = payment as CommonPaymentRequest;
                        result = _paymentCommonProcessor.Pay(commonPaymentRequest);

                        break;
                    case PaymentType.VPosCancellationRefund:

                        var _paymentVposCancelProcessor = PaymentProcessorFactory<IVposPaymentProcessor>.Create(VposPaymentSupplier.VPos);
                        VposRequest vposCancelRequest = payment as VposRequest;
                        result = _paymentVposCancelProcessor.CancellationRefund(vposCancelRequest);
                        break;
                }

                return result;
            }
            catch (PayFlexClientException ex)
            {
                throw ex;
            }
        }

        public PaymentQueryResponse Query(PaymentQueryRequest query)
        {
            try
            {
                PaymentQueryResponse result = null;

                switch (query.PaymentType)
                {
                    case PaymentType.CommonPayment:

                        var _paymentCommonProcessor = PaymentProcessorFactory<ICommonPaymentProcessor>.Create(VposPaymentSupplier.CommonPayment);
                        result = _paymentCommonProcessor.Query(query);

                        break;
                }

                return result;
            }
            catch (PayFlexClientException ex)
            {
                throw ex;
            }
        }
    }
}