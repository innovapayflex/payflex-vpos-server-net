using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
    public enum VposPaymentSupplier
    {
        MPI,
        VPos,
        CommonPayment,
    }

    public enum PaymentType
    {
        VPos,
        VPosCancellationRefund,
        MPI,
        CommonPayment,
    }

    public enum PaymentStatus
    {
        Success,
        UnSuccess
    }

    public enum TransactionType
    {
        Sale,
        Auth,
        Vft,
        Point,
        Cancel,
        Refund,
        Capture,
        Reversal,
        CampaignSearch,
        CardTest,
        BatchClosedSuccessSearch,
        SurchargeSearch,
        VFTSale,
        VFTSearch,
        PointSearch,
        PointSale,
        Credit
    } 

    public enum Currency
    {
        TRY = 949,
        USD = 840,
        EUR = 978,
        JPY = 392,
        GBP = 826,
    }

    public enum DeviceType
    {
        Android = 1,
        IOS = 2,
        Windows = 3
    }

    public enum TransactionDeviceSource
    {
        ECommerce = 0,
        MailOrder = 1
    }
    public enum MerchantType
    {
        Standart = 0,
        MainInfant = 1,
        SubInfant = 2
    }

    public enum BrandName
    {
        Visa = 100,
        MasterCard = 200
    }
}
