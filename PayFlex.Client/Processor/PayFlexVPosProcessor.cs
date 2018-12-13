using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Text;

namespace PayFlex.Client.Processor
{
    public class PayFlexVPosProcessor : IVposPaymentProcessor
    {

        /// <summary>
        ///  PayFlex Vpos satış işlemi.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public PaymentResponse Pay(VposRequest payment)
        {
            #region Pos Configuration             
            string strHostAddress = payment.ServiceUrl;
            #endregion

            var cardInfo = payment.CreditCard;

            var postData = new StringBuilder();
            postData.AppendFormat("{0}", "prmstr=<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            postData.AppendFormat("{0}", payment.ToXML());

            byte[] postByteArray = Encoding.UTF8.GetBytes(postData.ToString());
            
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
        /// Kayıt Detay Sorgulama işlemi
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PaymentResponse Search(VposSearchRequest input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Günlük, başarılı olarak gerçekleşmiş işlemlerin liste sorgulama
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PaymentResponse SettlementDetailQuery(SettlementDetailRequest input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Günlük, başarılı olarak gerçekleşmiş işlemlerin işlem tipi bazında sorgulama
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PaymentResponse SettlementQuery(SettlementRequest input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Belirli bir terminal e ait henüz kapatılmamış olan Batch numarası üzerinden, başarılı olarak gerçekleşmiş işlem sorgulama
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PaymentResponse SucceededOpenBatchTransactions(OpenBatchTransactionsRequest input)
        {
            throw new NotImplementedException();
        }
    }
}
