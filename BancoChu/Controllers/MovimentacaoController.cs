using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Services.Interfaces;
using BancoChu.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Controllers
{
    public class MovimentacaoController : Controller
    {
        private readonly ILogger<MovimentacaoController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IMovimentacaoServices _movimentacaoServices;

        public MovimentacaoController(
            ILogger<MovimentacaoController> logger,
            IMemoryCache cache,
            IMovimentacaoServices movimentacaoServices)
        {
            _logger = logger;
            _cache = cache;
            _movimentacaoServices = movimentacaoServices;
        }

        [HttpPost("MovimentarConta")]
        public IActionResult MovimentarConta(MovimentacaoDto movimento)
        {
            var success = _movimentacaoServices.MovimentarConta(movimento);

            if (success)
            {
                return Ok($"Conta criada com sucesso");
            }
            else
            {
                return BadRequest($"Já existe uma conta para este CpfCnpj");
            }
        }

    }
}
