using BancoChu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;

namespace BancoChu.Controllers
{
    [Route("[controller]")]
    public class ExtratoController : Controller
    {
        private readonly ILogger<ExtratoController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IExtratoServices _extratoServices;

        public ExtratoController(
            ILogger<ExtratoController> logger,
            IMemoryCache cache,
            IExtratoServices extratoServices)
        {
            _logger = logger;
            _cache = cache;
            _extratoServices = extratoServices;
        }

        [HttpGet("GetExtratoByCpfCnpj")]
        public IActionResult GetExtratBrPeriodo([Required] string cpfCnpj, [Required] string periodo)
        {
            var extrato = _extratoServices.GetExtratoByCpfCnpjPeriodo(cpfCnpj, periodo);
            return Ok(extrato);
        }
    }
}
