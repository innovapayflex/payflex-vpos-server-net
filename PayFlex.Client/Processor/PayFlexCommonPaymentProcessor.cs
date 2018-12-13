using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace PayFlex.Client.Processor
{
    public class PayFlexCommonPaymentProcessor : IPaymentProcessor<CommonPaymentRequest>, ICommonPaymentProcessor
    {
        /// <summary>
        /// Ortak Ödeme Sistemine İşlem Kayıt Etme.Ortak Ödeme sayfasında satış işlemine başlamadan önce işlem kaydedilmelidir.
        /// </summary>
        /// <param name="payment">İşlemi kaydetmek için Ortak Ödeme API Adresi belirtilen CommonPaymentRequest ile kullanılmaktadır. </param>
        /// <returns>API çağrısının sonucunda Ortak Ödeme sistemi bir GUID dönecektir. Bu GUID ile Ortak Ödeme Ekranı açılabilir</returns>
        public PaymentResponse Pay(CommonPaymentRequest payment)
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
        /// İşlem Sorgulama
        /// </summary>
        /// <param name="payment">PaymentToken veya TransactionId alanlarından birinin gönderilmesi yeterlidir. İkisinin aynı anda gönderim zorunluluğu yoktur.</param>
        /// <returns></returns>
        public PaymentQueryResponse Query(PaymentQueryRequest payment)
        {
            #region Pos Configuration           
            string strHostAddress = payment.ServiceUrl;
            #endregion

            var postData = new StringBuilder();
            postData.AppendFormat("{0}={1}&", "Password", payment.Password);
            postData.AppendFormat("{0}={1}&", "TransactionId", payment.TransactionId);
            postData.AppendFormat("{0}={1}&", "PaymentToken", payment.PaymentToken);

            byte[] postByteArray = Encoding.UTF8.GetBytes(postData.ToString());

            WebRequest webRequest = WebRequest.Create(strHostAddress);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postByteArray.Length;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | (SecurityProtocolType)768 | (SecurityProtocolType)3072 | SecurityProtocolType.Tls;

            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(postByteArray, 0, postByteArray.Length);
            dataStream.Close();

            WebResponse webResponse = webRequest.GetResponse();
            dataStream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            var queryResponse = new PaymentQueryResponse();
            //bankResponse.Response = reader.ReadToEnd();
            //bankResponse.IsSuccessful = true;
            reader.Close();
            dataStream.Close();
            webResponse.Close();

            return queryResponse;
        }

    }
}
