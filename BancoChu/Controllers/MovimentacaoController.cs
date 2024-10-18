using BancoChu.Entidades;
using BancoChu.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly ILogger<MovimentacaoController> _logger;
        private readonly IMemoryCache _cache;

        public MovimentacaoController(
            ILogger<MovimentacaoController> logger,
            IMemoryCache cache,
            IContasServices contasServices)
        {
            _logger = logger;
            _cache = cache;
        }


    }
}
