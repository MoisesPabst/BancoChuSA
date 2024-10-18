using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Services.Interfaces;
using BancoChu.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContasController : ControllerBase
    {

        private readonly ILogger<ContasController> _logger;
        private readonly IMemoryCache _cache;
        private readonly IContasServices _contasServices;

        public ContasController(
            ILogger<ContasController> logger, 
            IMemoryCache cache,
            IContasServices contasServices)
        {
            _logger = logger;
            _cache = cache;
            _contasServices = contasServices;
        }

        [HttpPost("CreateConta")]
        public IActionResult CreateConta(ContaDto conta)
        {
            var success = _contasServices.CreateConta(conta);

            if (success)
            {
                return Ok($"Conta criada com sucesso");
            }
            else
            {
                return BadRequest($"Já existe uma conta para este CpfCnpj");
            }
        }

        [HttpPut("UpdateConta")]
        public IActionResult UpdateConta(ContaDto conta)
        {
            var success = _contasServices.UpdateConta(conta);

            if (success)
            {
                return Ok($"Conta alterado com sucesso");
            }
            else
            {
                return BadRequest($"Não encontrada conta para alterar");
            }
        }

        [HttpDelete("DeleteConta")]
        public IActionResult DeleteConta(string CpfCnpj)
        {
            var success = _contasServices.DeleteConta(CpfCnpj);

            if (success)
            {
                return Ok($"Conta excluída com sucesso");
            } else
            {
                return BadRequest($"Não possível excluir a Conta");
            }
        }

        [HttpGet("GetAllContas")]
        public List<Contas> GetContas()
        {
            return _contasServices.GetContas();
        }
        [HttpGet("GetContaByCpfCnpj")]
        public Contas GetContaByCnpfCnpj(string CpfCnpj)
        {
            return _contasServices.GetContasByCpfCNPJ(CpfCnpj);
        }

        /*Função específica para realizar testes das implementações*/
        [HttpPost("CreateContaPadrao")]
        public void CreateContaPadrao()
        {
            ContaDto novaConta = new ContaDto();
            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés";
            novaConta.Email = "moises.pabst@gmail.com";
            novaConta.Saldo = 200.52M;

            _contasServices.CreateConta(novaConta);

            ContaDto novaConta2 = new ContaDto();
            novaConta2.CpfCnpj = "2";
            novaConta2.Nome = "Moisés2";
            novaConta2.Email = "moisespabst@gmail.com";
            novaConta2.Saldo = 250.52M;

            _contasServices.CreateConta(novaConta2);

            ContaDto novaConta3 = new ContaDto();
            novaConta3.CpfCnpj = "3";
            novaConta3.Nome = "Moisés3";
            novaConta3.Email = "moises.pabst.mp@gmail.com";
            novaConta3.Saldo = 415M;

            _contasServices.CreateConta(novaConta3);


        }
    }
}
