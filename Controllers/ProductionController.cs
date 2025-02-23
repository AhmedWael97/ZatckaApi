using Microsoft.AspNetCore.Mvc;

namespace ZatckaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionController : Controller
    {
        private readonly ILogger<ProductionController> _logger;

        public ProductionController(ILogger<ProductionController> logger)
        {
            _logger = logger;
        }
    }
}
