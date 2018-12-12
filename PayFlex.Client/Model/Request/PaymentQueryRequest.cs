using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Client
{
    public class PaymentQueryRequest : Payment
    {
        /// <summary>
        /// Üye işyeri şifresi
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Her işlem başına benzersiz olması gereken işlem numarasıdır.
        /// </summary>
        public string TransactionId { get; set; }
        /// <summary>
        /// Ortak Ödeme yapılan kayıt isteğine dönülen ve Ortak Ödeme ekranın açılması için gerekli olan token
        /// </summary>
        public string PaymentToken { get; set; }
    }
}
