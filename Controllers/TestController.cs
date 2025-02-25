using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Xml;
using Zatca.EInvoice.SDK.Contracts.Models;

using ZatckaAPI.Model;

namespace ZatckaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("/generate_csr")]
        public IActionResult generate_csr( [FromBody] CSRRquest cSRRquest)
        {             
            if(cSRRquest.CsrGenerationDto== null)
            {
                return BadRequest("csr cannot be null");
            }

           try
            {
                Zatca.EInvoice.SDK.CsrGenerator csrGenerator = new Zatca.EInvoice.SDK.CsrGenerator();
                CsrResult csrResult = csrGenerator.GenerateCsr(cSRRquest.CsrGenerationDto,cSRRquest.environmentType, cSRRquest.pemFormat);
                return Ok(csrResult);
            } catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error {e.Message}");
            }
        }

        [HttpPost]
        [Route("/generate_request")]
        public IActionResult Generate_request([FromBody] XmlInput EInvoice)
        {

            
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(EInvoice.XmlContent);
                Zatca.EInvoice.SDK.RequestGenerator requestGenerator = new Zatca.EInvoice.SDK.RequestGenerator();
                return Ok(requestGenerator.GenerateRequest(xmlDoc));
            } catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Erro ${e.Message}");
            }
        }


        [HttpPost]
        [Route("/generate_hash")]
        public IActionResult Generate_Hash([FromBody] XmlInput EInvoice)
        {
           

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(EInvoice.XmlContent);
                Zatca.EInvoice.SDK.EInvoiceHashGenerator eInvoiceHashGenerator = new Zatca.EInvoice.SDK.EInvoiceHashGenerator();
                HashResult hashResult = eInvoiceHashGenerator.GenerateEInvoiceHashing(xmlDoc);
                return Ok(JsonConvert.SerializeObject(hashResult));
            } catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error ${e.Message}");
            }
        }

        [HttpPost]
        [Route("/generate_qr_code")]
   
        public IActionResult generateQrCode([FromBody] XmlInput EInvoice)
        {
            

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(EInvoice.XmlContent);
                Zatca.EInvoice.SDK.EInvoiceQRGenerator eInvoiceQRGenerator = new Zatca.EInvoice.SDK.EInvoiceQRGenerator();
                QRResult qRResult = eInvoiceQRGenerator.GenerateEInvoiceQRCode(xmlDoc);
                return Ok(JsonConvert.SerializeObject(qRResult));
            } catch (Exception e)
            {
                return StatusCode(500, $"Internal Server Error ${e.Message}");
            }
        }

        [HttpPost]
        [Route("/sign_e_invoice")]
        public IActionResult signEInvoice([FromBody] SignRequest signRequest)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(signRequest.xmlDocument);
                Zatca.EInvoice.SDK.EInvoiceSigner eInvoiceSigner = new Zatca.EInvoice.SDK.EInvoiceSigner();
                SignResult signResult = eInvoiceSigner.SignDocument(xmlDoc, signRequest.certificate_content, signRequest.privateKeyContent);
                return Ok(JsonConvert.SerializeObject(signResult));
            } catch (Exception e)
            {
                return StatusCode(500, $"Internal Server error ${e.Message}");
            }
        }


        [HttpPost]
        [Route("/validate_e_invoice")]
        public IActionResult validateEInvoice([FromBody] ValidateInvoiceRequest validateInvoiceRequest)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(validateInvoiceRequest.xmlDocument);

                Zatca.EInvoice.SDK.EInvoiceValidator eInvoiceValidator = new Zatca.EInvoice.SDK.EInvoiceValidator();
                ValidationResult validationResult = eInvoiceValidator.ValidateEInvoice(xmlDoc, validateInvoiceRequest.certificateFileContent, validateInvoiceRequest.pihFileContent);
                return Ok(JsonConvert.SerializeObject(validationResult));
            } catch(Exception e)
            {
                return StatusCode(500, $"Internal Server Error : ${e.Message}");
            }
        }


        
    }
}
