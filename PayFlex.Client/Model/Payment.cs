
using System;
using System.Configuration;

namespace PayFlex.Client
{
    public class Payment
    {
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentType PaymentType { get; set; }
        /// <summary>
        /// İşlem adı bilgisi. 
        /// </summary>
        public TransactionType? TransactionType { get; set; }

        public string ServiceUrl { get; set; }
        /// <summary>
        /// PayFlex Üye iş yeri veya normal banka ödeme işlemi için xml request gönderip göndermeyeceği ile ilgili flag.
        /// </summary>
        public bool IsUPay { get; set; }

    }
}