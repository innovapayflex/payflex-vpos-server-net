using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
    public class MpiRequest : Payment
    {
        public MpiRequest()
        {
            PaymentType = PaymentType.MPI;
        }

        /// <summary>
        /// ÜİY Numarası
        /// Sayısal. Banka tarafından Üye işyerine iletilecektir. Üye işyeri için bu değer sabittir.
        /// </summary>
        public string MerchantId { get; set; }
        /// <summary>
        /// ÜİY Şifresi
        /// Sayısal. Banka tarafından Üye işyerine iletilecektir. Üye işyeri için bu değer config te tutulmalıdır. banka tarafından gerekli görüldüğü takdirde değiştirilebilecektir.
        /// </summary>
        public string MerchantPassword { get; set; }
        /// <summary>       
        /// ÜİY tarafından üretilen işlem numarasıdır. Her bir işlem için benzersiz olmalıdır. ÜİY, işlemlerini bu numara üzerinden takip edebilir.
        /// </summary>
        public string VerifyEnrollmentRequestId { get; set; }
        /// <summary>
        /// Kredi kartı bilgisi
        /// </summary>
        public CreditCard CreditCard { get; set; }
        /// <summary>
        /// Satış tutarı
        /// Satış tutarı ile küsuratı nokta işareti ile ayrılmalı, küsurat kısmı 2 hane olmalıdır. Bu alan en fazla 12 hane uzunluğunda olabilir.
        /// </summary>
        public decimal? PurchaseAmmount { get; set; }
        /// <summary>
        /// İşlemin yapıldığı sayısal para birimi kodu
        /// Sayısal – Tamsayı - 3 Hane. Bu alan boş iken işlem gönderildiğinde default olarak 949 CurrencyCode'a göre işlem gerçekleştirilmektedir.
        /// TL = 949,USD = 840,EUR = 978,JPY =3 92,GBP = 826
        /// </summary>
        public Currency? Currency { get; set; }
        /// <summary>
        /// Oturum Bilgisi
        /// ÜİY tarafında gönderilmesi Opsiyonel, sadece bilgi amaçlı tutulan bir alandır
        /// </summary>
        public string SessionInfo { get; set; }
        /// <summary>
        /// ÜİY'nin işlem sonucun başarılı olması durumunda, dönüş yapılmasını istediği sayfa URL.
        /// Eğer bu alan gönderilmediyse MPI, ÜİY için tanımlı SuccessUrl e dönüş yapacaktır
        /// </summary>
        public string SuccessUrl { get; set; }
        /// <summary>
        /// ÜİY'nin işlem sonucunun başarısız olması durumunda, dönüş yapılmasını istediği sayfa URL.
        /// Eğer bu alan gönderilmediyse MPI, ÜİY için tanımlı FailureUrl e dönüş yapacaktır
        /// </summary>
        public string FailureUrl { get; set; }
        /// <summary>
        /// Periyodik İşlem Flag'i
        /// ÜİY, periyodik olarak yinelenen bir işlem yapıyorsa bu alanı "true" değeriyle göndererek MPI'a işlemin periyodik olarak yinelenen bir işlem olduğunu bildirebilir. "true" gönderilmesi durumunda "RecurringEndDate", "RecurringFrequency" alanlarının gönderilmesi gereklidir.
        /// True: Periyodik İşlem , False: Periyodik İşlem Değil
        /// </summary>
        public string IsRecurring { get; set; }
        /// <summary>
        /// Periyodik İşlem Frekansı
        /// Sayısal – Tam Sayı Eğer işlem, periyodik olarak yinelenen bir satış ise, iki periyod arasındaki minimum gün sayısı, tamsayı olarak bu alanda gönderilmelidir. Aylık Periyodlarda bu değer 28 olarak gönderilmelidir.
        /// Örn: 25 - Koşullu (IsRecurring="true" ise zorunlu)
        /// </summary>
        public string RecurringFrequency { get; set; }
        /// <summary>
        /// Periyodik İşlem Bitiş Zamanı
        /// Sayısal – Tam Sayı – 8 Hane. Periyodik olarak yinelenen işlemin sonlanma tarihidir. YYYYAAGG formatında gönderilmelidir. Bu alandaki tarih, kartın son kullanma tarihinden büyükse ACS sunucusu işlemi reddeder.
        /// Örn: 20131023 - Koşullu (IsRecurring="true" ise zorunlu)
        /// </summary>
        public string RecurringEndDate { get; set; }
        /// <summary>
        /// Üye İşyeri Tipi
        /// Sayısal. Banka tarafından Üye işyerine iletilecektir. Üye işyeri için bu değer sabittir.
        /// </summary>
        public MerchantType? MerchantType { get; set; }
        /// <summary>
        /// Alt Bayi Numarası
        /// Alfanumeric. Banka tarafından AltBayiler için üye işyerine iletilecektir. Üye işyeri için bu değer sabittir.
        /// Örn: 00000000000471
        /// Koşullu: MerchantType:2 ise zorunlu, MerchantType:0 ise, gönderilmemeli MerchantType:1 ise, Ana bayi kendi adına işlem geçiyor ise gönderilmemeli, 
        /// Altbayisi adına işlem geçiyor ise zorunludur.
        /// </summary>
        public string SubMerchantId { get; set; }

        public override string ToString()
        {
            NumberFormatInfo moneyFormatInfo = new NumberFormatInfo();
            moneyFormatInfo.NumberDecimalSeparator = ".";

            var str = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(MerchantId))
                str.AppendFormat("{0}={1}&", "MerchantId", MerchantId);
            if (!string.IsNullOrWhiteSpace(MerchantPassword))
                str.AppendFormat("{0}={1}&", "MerchantPassword", MerchantPassword);
            if (TransactionType.HasValue)
                str.AppendFormat("{0}={1}&", "TransactionType", TransactionType.ToString());
            if (!string.IsNullOrWhiteSpace(VerifyEnrollmentRequestId))
                str.AppendFormat("{0}={1}&", "VerifyEnrollmentRequestId", VerifyEnrollmentRequestId);
            if (!string.IsNullOrWhiteSpace(CreditCard.Pan))
                str.AppendFormat("{0}={1}&", "Pan", CreditCard.Pan);
            if (!string.IsNullOrWhiteSpace(CreditCard.Expiry))
                str.AppendFormat("{0}={1}&", "ExpiryDate", CreditCard.Expiry);
            if (PurchaseAmmount.HasValue)
                str.AppendFormat("{0}={1}&", "PurchaseAmount", PurchaseAmmount.Value.ToString(moneyFormatInfo));
            if (Currency.HasValue)
                str.AppendFormat("{0}={1}&", "Currency", (int)Enum.Parse(typeof(Currency), Currency.ToString()));
            if (CreditCard.BrandName.HasValue)
                str.AppendFormat("{0}={1}&", "BrandName", (int)Enum.Parse(typeof(BrandName), CreditCard.BrandName.ToString()));
            if (!string.IsNullOrWhiteSpace(SessionInfo))
                str.AppendFormat("{0}={1}&", "SessionInfo", SessionInfo);
            if (!string.IsNullOrWhiteSpace(SuccessUrl))
                str.AppendFormat("{0}={1}&", "SuccessUrl", SuccessUrl);
            if (!string.IsNullOrWhiteSpace(FailureUrl))
                str.AppendFormat("{0}={1}&", "FailUrl", FailureUrl);
            if (CreditCard.NumberOfInstallments.HasValue)
                str.AppendFormat("{0}={1}&", "InstallmentCount", CreditCard.NumberOfInstallments);
            if (!string.IsNullOrWhiteSpace(IsRecurring))
                str.AppendFormat("{0}={1}&", "IsRecurring", IsRecurring);
            if (!string.IsNullOrWhiteSpace(RecurringFrequency))
                str.AppendFormat("{0}={1}&", "RecurringFrequency", RecurringFrequency);
            if (!string.IsNullOrWhiteSpace(RecurringEndDate))
                str.AppendFormat("{0}={1}&", "RecurringEndDate", RecurringEndDate);
            if (MerchantType.HasValue)
                str.AppendFormat("{0}={1}&", "MerchantType", (int)Enum.Parse(typeof(MerchantType), MerchantType.ToString()));
            if (!string.IsNullOrWhiteSpace(SubMerchantId))
                str.AppendFormat("{0}={1}&", "SubMerchantId", SubMerchantId);

            return str.ToString().Remove(str.ToString().Length - 1);
        }
    }
}
