using BancoChu.Entidades.Dto;
using BancoChu.Entidades;
using BancoChu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Controllers
{
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
        public ExtratoDto GetMovimentacoes(string cpfCnpj)
        {
            return _extratoServices.GetExtratoByCpfCnpj(cpfCnpj);
        }
    }
}
