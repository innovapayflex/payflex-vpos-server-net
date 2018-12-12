using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PayFlex.Client
{
    [XmlType(AnonymousType = true)]
    public partial class MPIResponse
    {
        [XmlElement(IsNullable = true, ElementName = "Message")]
        public Message Message { get; set; }
        [XmlElement(IsNullable = true, ElementName = "MessageErrorCode")]
        public string MessageErrorCode { get; set; }
        [XmlElement(IsNullable = true, ElementName = "ErrorMessage")]
        public string ErrorMessage { get; set; }

        public string ThreedHTMLPage()
        {
            if (string.IsNullOrWhiteSpace(Message.VERes.ACSUrl) &&
               string.IsNullOrWhiteSpace(Message.VERes.PaReq) &&
               string.IsNullOrWhiteSpace(Message.VERes.TermUrl) &&
               string.IsNullOrWhiteSpace(Message.VERes.MD))
            {
                return "3D-Secure dont verify enrollment";
            }

            string html = @"<html>" +
                                        "<head>" +
                                        "<title>iPay APM 3D-Secure İşlem Sayfası</title>" +
                                        "</head>" +
                                        "<body>" +
                                            "<form name=\"downloadForm\" action=\"" + Message.VERes.ACSUrl + "\"" + " method=\"POST\">" +
                                            " <!--		<noscript>--><br><br>" +
                                            "<div id=\"image1\" style=\"position:absolute; overflow:hidden; left:0px; top:0px; width:180px; height:180px; z-index:0\"><img src=\"http://sanalpos.innova.com.tr/images/basarili.png\" alt=\"\" title=\"\" border=0 width=180 height=180></div>" +
                                        "<center>" +
                                        "<h1>3-D Secure İşleminiz yapılıyor</h1>" +
                                        "<h2>" +
                                        "Tarayıcınızda Javascript kullanımı engellenmiştir." +
                                        "<br></h2>" +
                                        "<h3>" +
                                        "3D-Secure işleminizin doğrulama aşamasına geçebilmek için Gönder butonuna basmanız gerekmektedir." +
                                        "</h3>" +
                                        "<input type=\"submit\" value=\"Gönder\">" +
                                        "</center>" +
                                        "<!--</noscript>-->" +
                                        "<input type=\"hidden\" name=\"PaReq\" value=\"" + Message.VERes.PaReq + "\">" +
                                        "<input type=\"hidden\" name=\"TermUrl\" value=\"" + Message.VERes.TermUrl + "\">" +
                                        "<input type=\"hidden\" name=\"MD\" value=\"" + Message.VERes.MD + "\">" +
                                        "</form>" +
                                        "<SCRIPT LANGUAGE=\"Javascript\" >" +
                                        "   //document.downloadForm.submit();" +
                                        "</SCRIPT>" +
                                        "</body>" +
                                        "</html>";

            return html;
        }
    }

    [XmlType(AnonymousType = true)]
    public class Message
    {
        [XmlElement(IsNullable = true, ElementName = "VERes")]
        public VResponse VERes { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class VResponse
    {
        [XmlElement(IsNullable = true, ElementName = "Version")]
        public string Version { get; set; }
        [XmlElement(IsNullable = true, ElementName = "Status")]
        public string Status { get; set; }
        [XmlElement(IsNullable = true, ElementName = "PaReq")]
        public string PaReq { get; set; }
        [XmlElement(IsNullable = true, ElementName = "ACSUrl")]
        public string ACSUrl { get; set; }
        [XmlElement(IsNullable = true, ElementName = "TermUrl")]
        public string TermUrl { get; set; }
        [XmlElement(IsNullable = true, ElementName = "MD")]
        public string MD { get; set; }
        [XmlElement(IsNullable = true, ElementName = "ACTUALBRAND")]
        public string ActualBrand { get; set; }
    }
}
