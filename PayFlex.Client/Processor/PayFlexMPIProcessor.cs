using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace PayFlex.Client.Processor
{
    public class PayFlexMPIProcessor : IThreedProcessor
    {
        /// <summary>
        /// Kimlik Doğrulama İşlem
        /// PayFlex MPI(3-D Secure) Platformunda işlem akışı aşağıdaki adımlarla gerçekleştirilmektedir. 
        /// Kart sahibi, ÜİY Web Sitesinde satın alacağı ürünleri belirler ve işlemi sonlandırmak için ödeme sayfasına gelir. ÜİY Web Sitesi, kart sahibinden ödeme bilgilerini alır.
        /// </summary>
        /// <param name="payment">Enrollment(Kredi Kartı Kayıt) Kontrol İsteği</param>
        /// <returns></returns>
        public PaymentResponse Pay(MpiRequest payment)
        {
            #region Pos Configuration                        
            string strHostAddress = payment.ServiceUrl;
            #endregion  

            byte[] postByteArray = Encoding.UTF8.GetBytes(payment.ToString());
            
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            WebRequest webRequest = WebRequest.Create(strHostAddress);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postByteArray.Length;
            webRequest.UseDefaultCredentials = true;
            
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(postByteArray, 0, postByteArray.Length);
            dataStream.Close();

            WebResponse webResponse = webRequest.GetResponse();
            dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var bankResponse = new PaymentResponse();
            bankResponse.Response = reader.ReadToEnd();
            bankResponse.IsSuccessful = true;
            reader.Close();
            dataStream.Close();
            webResponse.Close();

            return bankResponse;
        }

        /// <summary>
        /// 3-D Secure İşlemler için 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PaymentResponse CompleteThreed(VposRequest payment)
        {
            #region Pos Configuration             
            string strHostAddress = payment.ServiceUrl;
            #endregion

            var cardInfo = payment.CreditCard;           

            var postData = new StringBuilder();
            postData.AppendFormat("{0}", "<?xml version=\"1.0\" encoding=\"utf - 8\"?>");
            postData.AppendFormat("<{0}>", "VposRequest");
            postData.AppendFormat("<{0}>{1}</{0}>", "MerchantId", payment.MerchantId);
            postData.AppendFormat("<{0}>{1}</{0}>", "Password", payment.Password);
            postData.AppendFormat("<{0}>{1}</{0}>", "TransactionId", payment.TransactionId);
            postData.AppendFormat("<{0}>{1}</{0}>", "TerminalNo", payment.TerminalNo);
            postData.AppendFormat("<{0}>{1}</{0}>", "TransactionType", payment.TransactionType.ToString());
            postData.AppendFormat("<{0}>{1}</{0}>", "CurrencyAmount", payment.CurrencyAmount);
            postData.AppendFormat("<{0}>{1}</{0}>", "SurchargeAmount", payment.SurchargeAmount);
            postData.AppendFormat("<{0}>{1}</{0}>", "CurrencyCode", payment.CurrencyCode);            
            postData.AppendFormat("<{0}>{1}</{0}>", "Pan", cardInfo.Pan);
            postData.AppendFormat("<{0}>{1}</{0}>", "Expiry", cardInfo.Expiry);
            postData.AppendFormat("<{0}>{1}</{0}>", "Cvv", cardInfo.CVV);         
            postData.AppendFormat("<{0}>{1}</{0}>", "CAVV", payment.CAVV);
            postData.AppendFormat("<{0}>{1}</{0}>", "ECI", payment.ECI);           
            postData.AppendFormat("<{0}>{1}</{0}>", "MpiTransactionId", payment.MpiTransactionId);        
            postData.AppendFormat("</{0}>", "VposRequest");

            byte[] postByteArray = Encoding.UTF8.GetBytes(postData.ToString());
            
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            WebRequest webRequest = WebRequest.Create(strHostAddress);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/xml";
            webRequest.ContentLength = postByteArray.Length;            

            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(postByteArray, 0, postByteArray.Length);
            dataStream.Close();

            WebResponse webResponse = webRequest.GetResponse();
            dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var bankResponse = new PaymentResponse();
            bankResponse.Response = reader.ReadToEnd();
            bankResponse.IsSuccessful = true;
            reader.Close();
            dataStream.Close();
            webResponse.Close();

            return bankResponse;
        }

    }
}
