# PayFlex Vpos Client C# .Net 

# Kullanım

### 3D MPI Satış İşlemi

#### Request

```C#
string serviceUrl = "http://sp-test.innova.com.tr/{Bank}/MpiWeb/MPI_Enrollment.aspx";

var paymentManger = new PayFlex.Client.PaymentManager();

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
var result = paymentManger.Pay(threedRequest);
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
         CurrencyAmount = 90.50,
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

### Ortal Ödeme Satış İşlemi

Ortak Ödeme sayfasında satış işlemine başlamadan önce işlem kaydedilmelidir. İşlemi kaydetmek için CommonPaymentRequest oluşturarak aşağıda belirtilen örnek te yöntem kullanılır. API çağrısının sonucunda Ortak Ödeme sistemi bir GUID dönecektir. Bu GUID ile ortak ödeme sayfası link sayfayı yönlendirebilir.

#### Request

```C#
string serviceUrl = "https://sp-test.innova.com.tr/{Bank}/CPTest";

string cpPageUrl = "https://sp-test.innova.com.tr/{Bank}/CPWeb/SecurePayment?Ptkn={0}&RequestPage=Payment";

var paymentManger = new PayFlex.Client.PaymentManager();            

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
 
var result = paymentManger.Pay(commonPaymentRequest);
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


