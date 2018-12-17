using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
    public class CommonPaymentRequest : Payment
    {
        public CommonPaymentRequest()
        {
            PaymentType = PaymentType.CommonPayment;
        }

        /// <summary>
        /// Üye işyeri numarası
        /// Örn: 655500056
        /// </summary>
        public string HostMerchantId { get; set; }
        /// <summary>
        /// İşlem kur bilgisi
        /// Örn: 840, 949
        /// </summary>
        public Currency? AmountCode { get; set; }
        /// <summary>
        /// İşlem tutar bilgisi küsuratları . ile ayrılmalıdır.
        /// Örn: 0.01 (1 kuruşluk işlem geçmek için)
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Üye işyeri şifresi
        /// </summary>
        public string MerchantPassword { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HostTerminalId { get; set; }
        /// <summary>
        /// Her işlem başına benzersiz olması gereken işlem numarası gönderilmemesi durumunda ortak ödeme tarafından yaratılacaktır.
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// Sipariş numarası, başarısız olan işlemler boyunca tekrar tekrar gönderilebilir. OrderId ye sahip bir işlem başarılı olduğunda Aynı OrderId ile bir daha işlem yapılamaz.
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// Sipariş açıklaması
        /// </summary>
        public string OrderDescription { get; set; }
        /// <summary>
        /// İşlemin 3D yapılıp yapılmayacağına dair flag
        /// </summary>
        public bool IsSecure { get; set; }
        /// <summary>
        /// Kart sahibi 3D Secure programına dahil değil ise işlemin Vposa gönderilip gönderilmeyeceği ile ilgili flag.
        /// </summary>
        public bool? AllowNotEnrolledCard { get; set; }
        /// <summary>
        /// Başarılı işlem dönüş sayfası
        /// </summary>
        public string SuccessUrl { get; set; }
        /// <summary>
        /// Başarısız işlem dönüş sayfası
        /// </summary>
        public string FailUrl { get; set; }
        /// <summary>
        /// Kredi kartı bilgisi
        /// </summary>
        public CreditCard CreditCard { get; set; }
        /// <summary>
        /// Ortak Ödeme uygulaması İngilizce ve Türkçe dil desteğine sahiptir. 
        /// Türkçe kullanım için, tr-TR; İngilizce kullanım için, en-US iletilmelidir. Gönderilmezse, sistem Türkçe gösterim yapacaktır.
        /// Örn: tr-TR
        /// </summary>
        public string RequestLanguage { get; set; }
        /// <summary>
        /// Üye işyeri tipi. Banka tarafından üye işyerine iletilecektir.
        /// alabileceği değerler: 0: Standart İşyeri, 1: Ana Bayi, 2: Alt Bayi
        /// </summary>
        public MerchantType? MerchantType { get; set; }
        /// <summary>
        /// Alt Bayi numarası. Banka tarafından alt bayi olarak tanımlı üye işyerleri için iletilecektir.	
        /// Örn: 0000000000471
        /// </summary>
        public string HostSubMerchantId { get; set; }

        public override string ToString()
        {
            NumberFormatInfo moneyFormatInfo = new NumberFormatInfo();
            moneyFormatInfo.NumberDecimalSeparator = ".";

            if (CreditCard == null)
            {
                CreditCard = new CreditCard();
            }


            var str = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(HostMerchantId))
                str.AppendFormat("{0}={1}&", "HostMerchantId", HostMerchantId);

            if (AmountCode.HasValue)
                str.AppendFormat("{0}={1}&", "AmountCode", (int)Enum.Parse(typeof(Currency), AmountCode.Value.ToString()));

            str.AppendFormat("{0}={1}&", "Amount", Amount.ToString(moneyFormatInfo));

            if (!string.IsNullOrWhiteSpace(MerchantPassword))
                str.AppendFormat("{0}={1}&", "MerchantPassword", MerchantPassword);
            if (!string.IsNullOrWhiteSpace(TransactionId))
                str.AppendFormat("{0}={1}&", "TransactionId", TransactionId);
            if (!string.IsNullOrWhiteSpace(OrderId))
                str.AppendFormat("{0}={1}&", "OrderID", OrderId);
            if (!string.IsNullOrWhiteSpace(OrderDescription))
                str.AppendFormat("{0}={1}&", "OrderDescription", OrderDescription);
            if (CreditCard.NumberOfInstallments.HasValue)
                str.AppendFormat("{0}={1}&", "InstallmentCount", CreditCard.NumberOfInstallments);
            if (TransactionType.HasValue)
                str.AppendFormat("{0}={1}&", "TransactionType", TransactionType.ToString());

            str.AppendFormat("{0}={1}&", "IsSecure", IsSecure.ToString().ToLowerInvariant());

            if (AllowNotEnrolledCard.HasValue)
                str.AppendFormat("{0}={1}&", "AllowNotEnrolledCard", AllowNotEnrolledCard.ToString().ToLowerInvariant());
            if (!string.IsNullOrWhiteSpace(SuccessUrl))
                str.AppendFormat("{0}={1}&", "SuccessUrl", SuccessUrl);
            if (!string.IsNullOrWhiteSpace(FailUrl))
                str.AppendFormat("{0}={1}&", "FailUrl", FailUrl);
            if (CreditCard.BrandName.HasValue)
                str.AppendFormat("{0}={1}&", "BrandNumber", (int)Enum.Parse(typeof(BrandName), CreditCard.BrandName.ToString()));
            if (!string.IsNullOrWhiteSpace(CreditCard.CVV))
                str.AppendFormat("{0}={1}&", "CVV", CreditCard.CVV);
            if (!string.IsNullOrWhiteSpace(CreditCard.Pan))
                str.AppendFormat("{0}={1}&", "PAN", CreditCard.Pan);
            if (!string.IsNullOrWhiteSpace(CreditCard.ExpireMonth))
                str.AppendFormat("{0}={1}&", "ExpireMonth", CreditCard.ExpireMonth);
            if (!string.IsNullOrWhiteSpace(CreditCard.ExpireYear))
                str.AppendFormat("{0}={1}&", "ExpireYear", CreditCard.ExpireYear);
            if (!string.IsNullOrWhiteSpace(RequestLanguage))
                str.AppendFormat("{0}={1}&", "RequestLanguage", RequestLanguage);
            if (MerchantType.HasValue)
                str.AppendFormat("{0}={1}&", "MerchantType", (int)Enum.Parse(typeof(MerchantType), MerchantType.ToString()));
            if (!string.IsNullOrWhiteSpace(HostTerminalId))
                str.AppendFormat("{0}={1}&", "HostTerminalId", HostTerminalId);
            if (!string.IsNullOrWhiteSpace(HostSubMerchantId))
                str.AppendFormat("{0}={1}&", "HostSubMerchantId", HostSubMerchantId);


            return str.ToString().Remove(str.ToString().Length - 1);
        }
    }
}
