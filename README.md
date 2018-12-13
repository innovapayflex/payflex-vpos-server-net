# PayFlex Vpos Client C# .Net 

# Kullanım

### 3D MPI Satış İşlemi

#### Request

```C#
string serviceUrl = "http://sp-test.innova.com.tr/{Bank}/MpiWeb/MPI_Enrollment.aspx";

var paymentManager = new PayFlex.Client.PaymentManager();

var threedRequest = new PayFlex.Client.MpiRequest()
{
    MerchantId = "000000000006528",
    MerchantPassword = "123456",
    ServiceUrl = serviceUrl,
    CreditCard = new CreditCard
    {
        Pan = "4938410160702981",
        CVV = "243",
        ExpireMonth = "03",
        ExpireYear = "24",
        BrandName = BrandName.Visa,
        CardHolderIp = "190.20.13.12"
    },
    Currency = Currency.TRY,
    VerifyEnrollmentRequestId = "000001",
    SessionInfo = "243",
    PurchaseAmmount = (decimal)10.01              

};
var result = paymentManager.Pay(threedRequest);
```

#### Response

```Xml
<IPaySecure>
<Message ID="crrz1no5d6b951b06fa043379458dc835b71d0c8">
		<VERes>
			<Version>1.0.2</Version>
			<Status>Y</Status>
      <PaReq>eJxVUltTszAQ/SsM75KEAKWdJY5364yO41cd+5gmqUVLaEOQ1l//JfSmT+zZPZy9nMD5ploG38o0Za2LkEQ4DJQWtSz1RxG2dn6Wh+cMJguj1PU/JVqjGDyqpuEfKihlEQpjfoiuU5nNhimZ4WzOcULpYJikuRQ5TWcDIrHIQwbPFy9qzWDfjLleUQzoAJ2qEQuuLQMu1pfjJ0ZiSggBtIdQKTO+ZlmaphjjNAO0S4DmlWLjJrjk+itwKoD6DIi61dZs2WDo2hwAtGbJFtauRgh1XReVWtffPBJ1FVn3p68COo3y3PqocWqbUrIpfdiqz9t29vOWTem043cLLSu7fKrqApBngORWsRgTSnCcBISMYjJKEkB9Hnjlx2CEYOz22gFY+R4Xvyu/M+BubpwlW5YnrnREoDarWivHcNsdY3jxHjGYG7Vue16cAzohUFruKP2MMXWSxxSgw/e09tW990NYd+j78nZy09583X2+T8hr2dB5xXVXFN6hnuBHKt2NvXA/kweAvATam4/2b8dFf97Uf2YU1oU=</PaReq>
			<ACSUrl> https://dropit.3dsecure.net:9443/PIT/ACS</ACSUrl>
			<TermUrl> http://www.innova.com.tr/MPI/MPI_Pares.asp</TermUrl>
			<MD>crrz1no5d6b951b06fa043379458dc835b71d0c8</MD>
			<ACTUALBRAND>Master</ACTUALBRAND>
		</VERes>
		</Message>
<MessageErrorCode>200</MessageErrorCode> 
</IPaySecure>
```

```C#
XmlRootAttribute xRoot = new XmlRootAttribute();
xRoot.ElementName = "IPaySecure";
xRoot.IsNullable = true;
string cleanXml = Regex.Replace(result.Response, @"<[a-zA-Z].[^(><.)]+/>", new MatchEvaluator(RemoveText));
MemoryStream memoryStream = new MemoryStream((new UTF8Encoding()).GetBytes(cleanXml));
XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
XmlSerializer xmlSerialize = new XmlSerializer(typeof(MPIResponse), xRoot);
var mpiResponse = (MPIResponse)xmlSerialize.Deserialize(memoryStream);

//3D enrollment satış işlem sayfası
var threedPage = mpiResponse.ThreedHTMLPage();

```
### Vpos Satış İşlemi

#### Request

```C#
string serviceUrl = "https://sp-test.innova.com.tr/{Bank}/VposWeb/v3/Vposreq.aspx";

var paymentManager = new PayFlex.Client.PaymentManager();

var vposRequest = new PayFlex.Client.VposRequest()
{
     MerchantId = "655500056",
     Password = "123456",
     TerminalNo = "1",
     TransactionType = TransactionType.Sale,
     ServiceUrl = serviceUrl,
     TransactionId = "b2d71cc5-d242-4b01-8479-d56eb8f74d7c", // opsiyonel
     CurrencyAmount = (decimal)10.00,
     CurrencyCode = Currency.TRY,
     CreditCard = new CreditCard
     {
     	Pan = "4543600299100712",
        CVV = "454",
        ExpireMonth = "11",
        ExpireYear = "2016",
        CardHolderIp = "190.20.13.12"
       },
       ECI = "05", //opsiyonel(TransactionDeviceSource 0 ise zorunlu, 1 ise, olmamalı) 
       CAVV = "asfa435redf", //opsiyonel(TransactionDeviceSource 0 ise zorunlu, 1 ise, olmamalı) 
       ExpSign = "131132451234314132", //opsiyonel --> BKM Ekspress işlemleri için, BKM Express ExpSign değeri bu alanda gönderilebilir. 
       OrderId = "z2d71cc5-d242-4b01-8479-d56eb8f74d7c", //opsiyonel
       Location = "1",
       Extract = "090020304", //opsiyonel
       DeviceType = DeviceType.Windows,
       TransactionDeviceSource = TransactionDeviceSource.ECommerce, //opsiyonel
};

var result = paymentManager.Pay(vposRequest);

```

#### Response
```Xml
<VposResponse>
<MerchantId>655500056</MerchantId> 
<TransactionType>Sale</TransactionType> 
<TransactionId>b2d71cc5-d242-4b01-8479-d56eb8f74d7c</TransactionId>            	
<OrderId>z2d71cc5-d242-4b01-8479-d56eb8f74d7c</OrderId> opsiyonel
<ResultCode>0000</ResultCode> 
<ResultDetail>İŞLEM BAŞARILI</ResultDetail> 
<AuthCode>11234</AuthCode> 
<HostDate>175017</HostDate> 
<Rrn>201101240006</Rrn> 
<CurrencyAmount>90.50</CurrencyAmount> 
<CurrencyCode>949</CurrencyCode> 
<ThreeDSecureType>1</ThreeDSecureType> 
<GainedPoint>0</GainedPoint> 
<TotalPoint>100</TotalPoint> 
<SurchargeAmount>92.50</SurchargeAmount> opsiyonel 
<Extract>090020304</Extract> opsiyonel 
<TLAmount>92.50</TLAmount> 
<CampaignResult> opsiyonel 
<CampaignItem action="KAT" code="" description="2" /> 
</CampaignResult> 
</VposResponse>
```

```C#
XmlRootAttribute xRoot = new XmlRootAttribute();
xRoot.ElementName = "VposResponse";
xRoot.IsNullable = true;
string cleanXml = Regex.Replace(result.Response, @"<[a-zA-Z].[^(><.)]+/>", 
new MatchEvaluator(RemoveText));

MemoryStream memoryStream = new MemoryStream((new UTF8Encoding()).GetBytes(cleanXml));
XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

XmlSerializer xmlSerialize = new XmlSerializer(typeof(VPosResponse), xRoot);
var vposResponse = (VPosResponse)xmlSerialize.Deserialize(memoryStream);
```

### Ortak Ödeme Satış İşlemi

Ortak Ödeme sayfasında satış işlemine başlamadan önce işlem kaydedilmelidir. İşlemi kaydetmek için CommonPaymentRequest oluşturarak aşağıda belirtilen örnek te yöntem kullanılır. API çağrısının sonucunda Ortak Ödeme sistemi bir GUID dönecektir. Bu GUID ile ortak ödeme sayfası link sayfayı yönlendirebilir.

#### Request

```C#
string serviceUrl = "https://sp-test.innova.com.tr/{Bank}/CPTest";

string cpPageUrl = "https://sp-test.innova.com.tr/{Bank}/CPWeb/SecurePayment?Ptkn={0}&RequestPage=Payment";

var paymentManager = new PayFlex.Client.PaymentManager();            

var commonPaymentRequest = new PayFlex.Client.CommonPaymentRequest()
{
    ServiceUrl = serviceUrl,
    TransactionId = Guid.NewGuid().ToString(),
    HostMerchantId = "000000000006528",
    MerchantPassword = "123456",
    HostTerminalId = "VP000095",
    OrderId = "cptest20140814o1",
    OrderDescription = "cptest20140814o1",
    Amount = (decimal)1.23,
    AmountCode = Currency.TRY,
    TransactionType = TransactionType.Sale,
    IsSecure = false,
    AllowNotEnrolledCard = false,
    SuccessUrl = "https://sp-test.innova.com.tr/VAKIFBANK_V4/CPTest/Success.aspx",
                FailUrl = "https://sp-test.innova.com.tr/VAKIFBANK_V4/CPTest/Fail.aspx",
     CreditCard = new CreditCard
     {
         Pan = "4402939925637022",
         CVV = "544",
         ExpireMonth = "02",
         ExpireYear = "2023",
         CardHolderIp = "190.20.13.12",
         BrandName = BrandName.Visa
     },
                RequestLanguage = "tr-TR"
    };
 
var result = paymentManager.Pay(commonPaymentRequest);
```

#### Request

```Html
                <td>Payment Token
                </td>
                <td>2667edfe-4bea-4cfb-a5e0-a9b401022bb4
                </td>
                <td colspan="2">
                  <a href="https://sp-test.innova.com.tr/{Bank}/CPWeb/SecurePayment?Ptkn=2667edfe-4bea-4cfb-a5e0-a9b401022bb4&RequestPage=Payment" target="_blank">Ödeme sayfasına git</a>
                </td>

```

```C#
string PaymentTokenNode = "";
string ErrorCodeNode = "";
string ErrorMessageNode = "";


var str = result.Response.Replace("{", "");
result.Response = str.Replace("}", "");
var stringArray = result.Response.Split(',');

if (stringArray.Length == 4)
{
    PaymentTokenNode = stringArray[1].Split(':')[1].Replace(@"""", "");
    ErrorCodeNode = stringArray[2].Split(':')[1].Replace(@"""", "");
    ErrorMessageNode = stringArray[3].Split(':')[1].Replace(@"""", "");
}

cpPageUrl = string.Format(cpPageUrl, PaymentTokenNode);
```

#### İşlem Sonuç Kodları
Başarılı işlem cevabı 0000'dır. Hatalı işlemler için Sanal POS'un ve Ortak Ödemenin döndürdüğü hata kodları aşağıdaki tablo da gösterilmektedir.

| Kod  | Açıklama  |
|----- | --------- |
| 0000  | Başarılı |
| 8484  | Birden fazla hata olması durumunda geri dönülür. ResultDetail alanından detayları alınabilir. |
| 1001  | Sistem hatası. |
| 1006  | Bu TransactionId ile daha önce başarılı bir işlem gerçekleştirilmiş |
| 1007  | Referans Transaction alınamadı |
| 1046  | İade işleminde tutar hatalı. |
| 1047  | İşlem tutarı geçersiz. |
| 1049  | Geçersiz tutar. |
| 1050  | CVV hatalı. |
| 1051  | Kredi kartı numarası hatalı. |
| 1052  | Kredi kartı son kullanma tarihi hatalı. |
| 1054  | İşlem numarası hatalı. |
| 1059  | Yeniden iade denemesi. |
| 1060  | Hatalı taksit sayısı. |
| 2200  | İş yerinin işlem için gerekli hakkı yok. |
| 2202  | İşlem iptal edilemez. ( Batch Kapalı ) |
| 5001  | İş yeri aktif değil. |
| 5002  | İş yeri şifresi yanlış. |
| 1073  | Terminal üzerinde aktif olarak bir Batch bulunamadı. |
| 1074  | İşlem henüz sonlanmamış ya da referans işlem henüz tamamlanmamış. |
| 1075  | Sadakat puan tutarı hatalı |
| 1076  | Sadakat puan kodu hatalı |
| 1077  | Para kodu hatalı |
| 1078  | Geçersiz sipariş numarası |
| 1079  | Geçersiz sipariş açıklaması |
| 1080  | Sadakat tutarı ve para tutarı gönderilmemiş. |
| 1061  | Aynı sipariş numarasıyla daha önceden başarılı işlem yapılmış |
| 1065  | Ön provizyon daha önceden kapatılmış |
| 1082  | Geçersiz işlem tipi |
| 1083  | Referans işlem daha önceden iptal edilmiş. |
| 7777  | Banka tarafında gün sonu yapıldığından işlem gerçekleştirilemedi |
| 1087  | Yabancı para birimiyle taksitli provizyon kapama işlemi yapılamaz |
| 1088  | Ön provizyon iptal edilmiş |
| 1089  | Referans işlem yapılmak istenen işlem için uygun değil |
| 1090  | Bölüm numarası bulunamıyor |
| 1091  | Recurring işlemin toplam taksit sayısı hatalı |
| 1092  | Recurring işlemin tekrarlama aralığı hatalı |
| 1093  | Sadece Satış (Sale) işlemi Recurring olarak işaretlenebilir |
| 1001  | Sistem hatası. |
| 1006  | Bu TransactionId ile daha önce başarılı bir işlem gerçekleştirilmiş |
| 1095  | Lütfen geçerli bir Email adresi giriniz |
| 1096  | Lütfen geçerli bir IP adresi giriniz |
| 1097  | Lütfen geçerli bir CAVV değeri giriniz |
| 1098  | Lütfen geçerli bir ECI değeri giriniz. |
| 1099  | Lütfen geçerli bir Kart Sahibi ismi giriniz. |
| 1100  | Lütfen geçerli bir Brand girişi yapın. |
| 1102  | Recurring işlem aralık tipi hatalı bir değere sahip |
| 5003  | İşlem bulunamadı |
| 5004  | Kredi Kartı sağlayıcısı bulunamadı |
| 5005  | Kredi kartının ilk 6 karakteri geçersiz |
| 5006  | Hatalı kart numarası |
| 5007  | Hatalı Cvv numarası |
| 5009  | TransactionId daha önce kullanılmış |
| 5010  | Bu işlem daha önce kullanılmış |
| 5011  | Bu işlem daha önce kullanılmış |
| 5012  | Hangi sayfanın talep edildiği anlaşılamadı |
| 5013  | Geçersiz TransactionId |
| 5014  | Geçersiz işlem tutarı |
| 5015  | Geçersiz para kodu |
| 5016  | Geçersiz sipariş detay bilgisi |
| 5017  | Geçersiz sipariş numarası |
| 5018  | Eksik parametre |
| 5019  | Geçersiz parametre uzunluğu |
| 5020  | Üye işyeri numarası eksik |
| 5021  | Ödeme bileti eksik |
| 5022  | Üye iş yeri şifresi eksik |
| 5023  | Geçersiz üye iş yeri numarası | 
| 5024  | Geçersiz işlem numarası uzunluğu | 
| 5025  | Geçersiz sipariş numarası uzunluğu | 
| 5026  | Geçersiz taksit sayısı | 
| 5027  | Geçersiz işlem tipi | 
| 5028  | Geçersiz işlem tipi | 
| 5029  | Geçersiz istek | 
| 5030  | Geçersiz HostMerchantId | 
| 5031  | Geçersiz TransactionId | 
| 5032  | Response değeri alınamadı | 
| 5033  | Register mesajı bulunamadı | 
| 5034  | Geçersiz PaymentToken | 
| 5035  | İşlem No desteklenmeyen karakterler içermektedir | 
| 5036  | Sipariş No desteklenmeyen karakterler içermektedir | 
| 5037  | Başarılı İşlem Adresi boş ya da hatalı | 
| 5038  | Başarısız İşlem Adresi boş ya da hatalı | 
| 5039  | İşyerinin Puan Satış yetkisi mevcut değil | 
| 5040  | TerminalNo boş ya da hatalı | 
| 6969  | İşlem ACS doğrulamasından geçemedi | 
| 9995  | Geçersiz dil |


