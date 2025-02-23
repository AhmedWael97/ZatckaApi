using System.Xml;

namespace ZatckaAPI.Model
{
    public class SignRequest
    {
       public String xmlDocument { get; set; }

      public string certificate_content { get; set; }

      public string privateKeyContent { get; set; }
    }
}
