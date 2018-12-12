using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
    /// <summary>
    /// 
    /// PayFlex VPos uygulamasına gönderilen XML isteği alanlarını içerir.
    /// </summary>
    public class VposRequest : Payment
    {
        public VposRequest()
        {
            PaymentType = PaymentType.VPos;
        }

        /// <summary>
        /// Üye işyeri numarası. Alfanumerik-15
        /// </summary>
        public string MerchantId { get; set; }
        /// <summary>
        /// Üye işyeri şifresi. Alfanumerik + Sembol
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Benzesiz (Unique) İşlem numara bilgisi.Alfanumerik-40
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// İşlemin hangi terminal üzerinden gönderileceği bilgisi.Alfanumerik-8
        /// ÜİY için tanımlanmış mevcut terminallerden herhangi birinin terminal numarası iletilmelidir. Ör: VB007000,…
        /// </summary>
        public string TerminalNo { get; set; }
        /// <summary>
        /// İşlem tutar bilgisi. Sayısal(kuruş hariç maks 10 basamaklı olabilir.)
        /// Örnek: 55.00 ya da 145.34
        /// </summary>
        public decimal? CurrencyAmount { get; set; }
        /// <summary>
        /// Surchargelı işlem tutar bilgisi.Sayısal(kuruş hariç maks 10 basamaklı olabilir.)
        /// Örnek: 55.00 ya da 145.34
        /// </summary>
        public decimal? SurchargeAmount { get; set; }
        /// <summary>
        /// İşlem kur bilgisi (YTL = 949).Sayısal – Tamsayı - 3
        /// TL = 949,USD = 840,EUR = 978,JPY =3 92,GBP = 826
        /// </summary>
        public Currency? CurrencyCode { get; set; }
        /// <summary>
        /// Kredi kartı bilgisi
        /// </summary>
        public CreditCard CreditCard { get; set; }
        /// <summary>
        /// Orjinal işlem üye işyeri işlem numarası bilgisi. Alfanumerik–40
        /// </summary>
        public string ReferenceTransactionId { get; set; }
        /// <summary>
        /// Sipariş numarası işlem başarılı olana kadar aynı sipariş numarası sisteme gönderilebilir(UyeRefNo).Alfanumerik–40 + Sembol
        /// </summary>
        public string OrderId { get; set; }
        /// <summary>
        /// Sipariş ile ilgili varsa detaylı açıklama.Alfanumerik- 2000 + Sembol
        /// </summary>
        public string OrderDescription { get; set; }
        /// <summary>
        /// İşlemin hangi periyod tipinde tekrarlanacağı bilgisini içerir. Alfanumerik-Sabit
        /// Day, (Günlük tekrarlama) - Month, (Aylık tekrarlama) - Year(Yıllık tekrarlama)
        /// </summary>
        public string RecurringFrequencyType { get; set; }
        /// <summary>
        /// İşlemin ne kadar sürede bir tekrarlanacağı bu alanda belirtilir. RecurringFrequencyType ile birlikte bir anlam ifade eder.
        /// </summary>
        public int? RecurringFrequency { get; set; }
        /// <summary>
        /// Recurring işlemin toplamda kaç kere tekrar edeceği bilgisini içerir.
        /// </summary>
        public int? RecurringInstallmentCount { get; set; }
        /// <summary>
        /// MPI tarafından 3D Secure işlemin sonucunda gönderilen alan.Alfanumerik- 28 + Sembol
        /// </summary>
        public string CAVV { get; set; }
        /// <summary>
        /// 3D secure işlemin sonucu.Sayısal. 
        /// Visa:05,06,07 - MasterCard:01,02,03
        /// </summary>
        public string ECI { get; set; }
        /// <summary>
        /// Üye işyeri tarafından işleme ait ek bilgiler varsa CustomItems alanında gönderilir. İçeriğinde "Item" listesi vardır.
        /// İşleme ait özel bilgiler içerir. İçeriğinde "name" ve "value" attirbutelarını barındırır.
        /// Name alanı maksimum 100 karakter desteklemektedir. aşıldığı durumda 1131 cevap kodu dönülmektedir.
        /// Value alanı maksimum 100 karakter desteklemektedir. aşıldığı durumda 1131 cevap kodu dönülmektedir.
        /// </summary>
        public Dictionary<string, string> CustomItems { get; set; }
        /// <summary>
        /// Raporlama amacıyla işlemin hangi kaynaktan gönderildiği bilgisi
        /// 1: Android, 2: IOS, 3: Windows
        /// </summary>
        public DeviceType? DeviceType { get; set; }
        /// <summary>
        /// MailOrder veye Ecommerce. MailOrder yetkiye bağlıdır ve yetkiniz yoksa sanalpos tarafnıdan işleminiz red edilecektir. Tüm Vpos İsteklerinde zorunludur.
        /// 0 : ECommerce , 1 : MailOrder
        /// </summary>
        public TransactionDeviceSource? TransactionDeviceSource { get; set; }
        /// <summary>
        /// Bu alanda gönderilecek değer kart hamili ektresinde işlem açıklamasında çıkacaktır. (Abone no vb. bilgiler gönderilebilir).
        /// </summary>
        public string Extract { get; set; }
        /// <summary>
        /// BKM Express İşlem Numarası. BKM Express işlemlerinde gönderilecektir. Mpi Sürecinden geçmiş olan işlemlerde bu alanın gönderilmemesi beklenmektedir. Alfanumerik-40
        /// </summary>
        public string ExpSign { get; set; }
        /// <summary>
        /// İşlemin Mpi tarafındaki TransactionId numarası. Bu değer VPOS tarafından işlemin 3d bilgileirni kontrol etmek için kullanılır.
        /// </summary>
        public string MpiTransactionId { get; set; }
        /// <summary>
        /// Raporlama amacıyla, işlemin gönderildiği yer bilgisi.Sayısal.
        /// 1:internet, 2:Telefon, 3:Mobile, 4:Kurum muhasebe/erp sistemi, 6:Kiosk, 7:Diğer
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Üye İşyeri tipi. Banka tarafından Üye işyerine iletilmektedir. üye işyeri için sabit bir değerdir. tüm isteklerde, bu değer kullanılmalıdır.
        ///0: Standart İşyeri, 1: Ana Bayi, 2: Alt Bayi
        /// </summary>
        public MerchantType? MerchantType { get; set; }
        /// <summary>
        /// Alt bayi numarası. Banka tarafından üye işyerine iletilmektedir. tüm isteklerde, üye işyeri kendisine iletilen bu değeri kullanmalıdır.
        /// Ör: 00000000000471
        /// </summary>
        public string SubMerchantId { get; set; }

        public string ToXML()
        {
            NumberFormatInfo moneyFormatInfo = new NumberFormatInfo();
            moneyFormatInfo.NumberDecimalSeparator = ".";

            StringBuilder strXML = new StringBuilder();
            strXML.AppendFormat("<{0}>", "VposRequest");
            if (!string.IsNullOrWhiteSpace(MerchantId))
                strXML.AppendFormat("<{0}>{1}</{0}>", "MerchantId", MerchantId);
            if (!string.IsNullOrWhiteSpace(Password))
                strXML.AppendFormat("<{0}>{1}</{0}>", "Password", Password);
            if (!string.IsNullOrWhiteSpace(TransactionId))
                strXML.AppendFormat("<{0}>{1}</{0}>", "TransactionId", TransactionId);
            if (!string.IsNullOrWhiteSpace(TerminalNo))
                strXML.AppendFormat("<{0}>{1}</{0}>", "TerminalNo", TerminalNo);
            if (TransactionType.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "TransactionType", TransactionType.ToString());
            if (CurrencyAmount.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "CurrencyAmount", CurrencyAmount.Value.ToString(moneyFormatInfo));
            if (SurchargeAmount.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "SurchargeAmount", SurchargeAmount.Value.ToString(moneyFormatInfo));
            if (CurrencyCode.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "CurrencyCode", (int)Enum.Parse(typeof(Currency), CurrencyCode.ToString()));
            if (!string.IsNullOrWhiteSpace(CreditCard.PointAmount))
                strXML.AppendFormat("<{0}>{1}</{0}>", "PointAmount", CreditCard.PointAmount);
            if (!string.IsNullOrWhiteSpace(CreditCard.PointCode))
                strXML.AppendFormat("<{0}>{1}</{0}>", "PointCode", CreditCard.PointCode);
            if (CreditCard.NumberOfInstallments.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "NumberOfInstallments", CreditCard.NumberOfInstallments);
            if (CreditCard.BrandName.HasValue)
                strXML.AppendFormat("{0}={1}&", "BrandName", (int)Enum.Parse(typeof(BrandName), CreditCard.BrandName.ToString()));
            if (!string.IsNullOrWhiteSpace(CreditCard.Pan))
                strXML.AppendFormat("<{0}>{1}</{0}>", "Pan", CreditCard.Pan);
            if (!string.IsNullOrWhiteSpace(CreditCard.Expiry))
                strXML.AppendFormat("<{0}>{1}</{0}>", "Expiry", CreditCard.Expiry);
            if (!string.IsNullOrWhiteSpace(CreditCard.CVV))
                strXML.AppendFormat("<{0}>{1}</{0}>", "Cvv", CreditCard.CVV);
            if (!string.IsNullOrWhiteSpace(CreditCard.SecurityCode))
                strXML.AppendFormat("<{0}>{1}</{0}>", "SecurityCode", CreditCard.SecurityCode);
            if (!string.IsNullOrWhiteSpace(ReferenceTransactionId))
                strXML.AppendFormat("<{0}>{1}</{0}>", "ReferenceTransactionId", ReferenceTransactionId);
            if (!string.IsNullOrWhiteSpace(CreditCard.CardHolderName))
                strXML.AppendFormat("<{0}>{1}</{0}>", "CardHoldersName", CreditCard.CardHolderName);
            if (!string.IsNullOrWhiteSpace(CreditCard.CardHolderIp))
                strXML.AppendFormat("<{0}>{1}</{0}>", "ClientIp", CreditCard.CardHolderIp);
            if (!string.IsNullOrWhiteSpace(OrderId))
                strXML.AppendFormat("<{0}>{1}</{0}>", "OrderId", OrderId);
            if (!string.IsNullOrWhiteSpace(OrderDescription))
                strXML.AppendFormat("<{0}>{1}</{0}>", "OrderDescription", OrderDescription);
            if (!string.IsNullOrWhiteSpace(RecurringFrequencyType))
                strXML.AppendFormat("<{0}>{1}</{0}>", "RecurringFrequencyType", RecurringFrequencyType);
            if (RecurringFrequency.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "RecurringFrequency", RecurringFrequency);
            if (RecurringInstallmentCount.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "RecurringInstallmentCount", RecurringInstallmentCount);
            if (!string.IsNullOrWhiteSpace(CAVV))
                strXML.AppendFormat("<{0}>{1}</{0}>", "CAVV", CAVV);
            if (!string.IsNullOrWhiteSpace(ECI))
                strXML.AppendFormat("<{0}>{1}</{0}>", "ECI", ECI);
            if (CustomItems != null)
                strXML.AppendFormat("<{0}>{1}</{0}>", "CustomItems", CustomItems);
            if (DeviceType.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "DeviceType", (int)Enum.Parse(typeof(DeviceType), DeviceType.ToString()));
            if (TransactionDeviceSource.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "TransactionDeviceSource", (int)Enum.Parse(typeof(TransactionDeviceSource), TransactionDeviceSource.ToString()));
            if (!string.IsNullOrWhiteSpace(Extract))
                strXML.AppendFormat("<{0}>{1}</{0}>", "Extract", Extract);
            if (!string.IsNullOrWhiteSpace(ExpSign))
                strXML.AppendFormat("<{0}>{1}</{0}>", "ExpSign", ExpSign);
            if (!string.IsNullOrWhiteSpace(MpiTransactionId))
                strXML.AppendFormat("<{0}>{1}</{0}>", "MpiTransactionId", MpiTransactionId);
            if (!string.IsNullOrWhiteSpace(Location))
                strXML.AppendFormat("<{0}>{1}</{0}>", "Location", Location);
            if (MerchantType.HasValue)
                strXML.AppendFormat("<{0}>{1}</{0}>", "MerchantType", (int)Enum.Parse(typeof(MerchantType), MerchantType.ToString()));
            if (!string.IsNullOrWhiteSpace(SubMerchantId))
                strXML.AppendFormat("<{0}>{1}</{0}>", "SubMerchantId", SubMerchantId);
            strXML.AppendFormat("</{0}>", "VposRequest");

            return strXML.ToString();
        }
    }
}
