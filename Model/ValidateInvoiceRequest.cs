using System.Xml;

namespace ZatckaAPI.Model
{
    public class ValidateInvoiceRequest
    {
        public String xmlDocument { get; set; }

        public string certificateFileContent { get; set; }

        public string pihFileContent { get; set; }
    }
}
