using NUnit.Framework;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PayFlex.Client.UnitTest
{
    [TestFixture]
    public class PaymentProcessorTest
    {
        //+ Test_Vpos_Post_Payment_Then_Return_GetAnyResponse
        //+ Test_Threed_Post_Payment_Then_Return_GetAnyResponse
        //+ Test_CommonPayment_Post_Payment_Then_Return_GetAnyResponse

        private PaymentManager _paymentManager;

        [OneTimeSetUp]
        public void Init()
        {
            _paymentManager = new PaymentManager();
        }

        [Test]
        public void Test_Vpos_Post_Payment_Then_Return_GetAnyResponse()
        {
            string serviceUrl = "https://sp-test.innova.com.tr/VAKIFBANK_V4/VposWeb/v3/Vposreq.aspx";           

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

            var result = _paymentManager.PostProcess(vposRequest);
            Assert.AreNotEqual("", result.Response);


            #region XML Response Deserialize
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.ElementName = "VposResponse";
            xRoot.IsNullable = true;
            string cleanXml = Regex.Replace(result.Response, @"<[a-zA-Z].[^(><.)]+/>", new MatchEvaluator(RemoveText));
            MemoryStream memoryStream = new MemoryStream((new UTF8Encoding()).GetBytes(cleanXml));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            XmlSerializer xmlSerialize = new XmlSerializer(typeof(VPosResponse), xRoot);
            var vposResponse = (VPosResponse)xmlSerialize.Deserialize(memoryStream);
            #endregion
        }

        [Test]
        public void Test_VPos_Cancellation_Refund_Then_Return_GetAnyResponse()
        {
            string serviceUrl = "https://sp-test.innova.com.tr/VAKIFBANK_V4/VposWeb/v3/Vposreq.aspx";

            var vposRequest = new PayFlex.Client.VposRequest()
            {
                PaymentType = PaymentType.VPosCancel,
                MerchantId = "655500056",
                Password = "123456",                
                TransactionType = TransactionType.Cancel,
                ServiceUrl = serviceUrl,
                ReferenceTransactionId = "b2d71cc5-d242-4b01-8479-d56eb8f74d7c", // opsiyonel
                ClientIp = "127.0.0.1",
                Location = "1",
                DeviceType = DeviceType.Android                
            };

            var result = _paymentManager.PostProcess(vposRequest);
            Assert.AreNotEqual("", result.Response);
        }

        [Test]
        public void Test_Threed_Post_Payment_Then_Return_GetAnyResponse()
        {
            string serviceUrl = "http://sp-test.innova.com.tr/VAKIFBANK/MpiWeb/MPI_Enrollment.aspx";            

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

            var result = _paymentManager.PostProcess(threedRequest);

            Assert.AreNotEqual("", result.Response);


            #region XML Response Deserialize
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
            #endregion
        }

        [Test]
        public void Test_CommonPayment_Post_Payment_Then_Return_GetAnyResponse()
        {
            string serviceUrl = "https://sp-test.innova.com.tr/VAKIFBANK_V4/CPTest";

            string cpPageUrl = "https://sp-test.innova.com.tr/VAKIFBANK_v4/CPWeb/SecurePayment?Ptkn={0}&RequestPage=Payment";           

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

            var result = _paymentManager.PostProcess(commonPaymentRequest);

            Assert.AreNotEqual("", result.Response);

            #region HtmlNode Parse
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
            #endregion
        }

        static string RemoveText(Match m) { return ""; }
    }
}
