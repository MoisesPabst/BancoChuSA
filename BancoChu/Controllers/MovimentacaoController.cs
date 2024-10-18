using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Entidades.Enums;
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
                return Ok($"Movimentação realizada com sucesso");
            }
            else
            {
                return BadRequest($"Conta origem e Destino são as mesmas");
            }
        }

        [HttpGet("GetMovimentacoes")]
        public List<Movimentacao> GetMovimentacoes()
        {
            return _movimentacaoServices.GetMovimentacao();
        }

        /*Função específica para realizar testes das implementações*/
        [HttpPost("CreateMovimentacaoPadrao")]
        public void CreateMovimentacaoPadrao()
        {
            MovimentacaoDto movi1 = new MovimentacaoDto();
            movi1.Descricao = "Primeira";
            movi1.CpfCnpj = "1";
            movi1.Valor = 24;
            movi1.ChavePix = "2";
            movi1.Tipo = TipoMovimentacaoEnum.Entrada;

            _movimentacaoServices.MovimentarConta(movi1);

            MovimentacaoDto movi2 = new MovimentacaoDto();
            movi2.Descricao = "Segunda";
            movi2.CpfCnpj = "1";
            movi2.Valor = 29;
            movi2.ChavePix = "2";
            movi2.Tipo = TipoMovimentacaoEnum.Saida;

            _movimentacaoServices.MovimentarConta(movi2);

            MovimentacaoDto movi3 = new MovimentacaoDto();
            movi3.Descricao = "Terceira";
            movi3.CpfCnpj = "1";
            movi3.Valor = 75;
            movi3.ChavePix = "3";
            movi3.Tipo = TipoMovimentacaoEnum.Entrada;

            _movimentacaoServices.MovimentarConta(movi3);

            MovimentacaoDto movi4 = new MovimentacaoDto();
            movi4.Descricao = "Quarta";
            movi4.CpfCnpj = "1";
            movi4.Valor = 98;
            movi4.IdContaDestino = 3;
            movi4.AgenciaDestino = 1278;
            movi4.Tipo = TipoMovimentacaoEnum.Saida;

            _movimentacaoServices.MovimentarConta(movi4);
        }
    }
}
