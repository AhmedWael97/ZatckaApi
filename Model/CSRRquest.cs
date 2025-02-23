using Zatca.EInvoice.SDK.Contracts.Models;
namespace ZatckaAPI.Model
{
    public class CSRRquest
    {
        public CsrGenerationDto CsrGenerationDto { get; set; }

        public EnvironmentType environmentType { get; set; }

        public bool pemFormat {get; set; }

    }
}
