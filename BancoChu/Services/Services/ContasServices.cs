using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Entidades.Enums;
using BancoChu.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Services.Services
{
    public class ContasServices : IContasServices
    {
        private readonly IMemoryCache _cache;
        private int Agencia = 1278;
        public ContasServices(IMemoryCache cache) { 
            _cache = cache;
        }

        public bool CreateConta(ContaDto contaDto)
        {
            var success = false;
            var conta = ConvertDtoToEntidade(contaDto);
            var listContas = GetContas();

            var contaExistente = listContas.Where(x => x.CpfCnpj == conta.CpfCnpj).FirstOrDefault();

            if (contaExistente == null)
            {
                success = true;
                conta.Conta = (listContas.Count == 0 || listContas.Max(x => x.Conta) == 0) ? 1 : listContas.Max(x => x.Conta) + 1;
                listContas.Add(conta);
                _cache.Set("Contas", listContas);
            }

            return success;
        }

        public bool UpdateConta(ContaDto conta)
        {
            var success = false;
            var listContas = GetContas();
            var contaExistente = listContas.Find(x => x.CpfCnpj == conta.CpfCnpj);

            if (contaExistente != null)
            {
                success = true;
                contaExistente.Nome = conta.Nome;
                contaExistente.Email = conta.Email;
                contaExistente.DataAlterado = DateTime.Now;
            }

            _cache.Set("Contas", listContas);
            return success;
        }

        public bool DeleteConta(string CpfCnpj)
        {
            var success = false;
            var listContas = GetContas();
            var contaExistente = listContas.Find(x => x.CpfCnpj == CpfCnpj);

            if (contaExistente != null)
            {
                success = true;
                listContas.Remove(contaExistente);
            }

            _cache.Set("Contas", listContas);
            return success;
        }

        public void MovimentaSaldo(Movimentacao movimentacao)
        {
            var listContas = GetContas();

            var contaOrigem = listContas.Find(x => x.Conta == movimentacao.IdConta && x.Agencia == movimentacao.Agencia);
            if (contaOrigem != null)
            {
                if(movimentacao.Tipo == TipoMovimentacaoEnum.Saida)
                {
                    contaOrigem.Saldo = contaOrigem.Saldo - movimentacao.Valor;
                }                    
                else
                {
                    contaOrigem.Saldo = contaOrigem.Saldo + movimentacao.Valor;
                }
            }

            var contaDestino = listContas.Find(x => x.Conta == movimentacao.IdContaDestino && x.Agencia == movimentacao.AgenciaDestino);
            if (contaDestino != null)
            {
                if (movimentacao.Tipo == TipoMovimentacaoEnum.Saida)
                {
                    contaDestino.Saldo = contaDestino.Saldo + movimentacao.Valor;
                }
                else
                {
                    contaDestino.Saldo = contaDestino.Saldo - movimentacao.Valor;
                }
            }

            _cache.Set("Contas", listContas);

        }

        public List<Contas> GetContas()
        {
            return _cache.Get("Contas") == null ? new List<Contas>() : (List<Contas>)_cache.Get("Contas");
        }

        public Contas GetContasByCpfCNPJ(string CpfCnpj)
        {
            var conta = GetContas().Where(x => x.CpfCnpj == CpfCnpj).FirstOrDefault();
            return conta != null ? conta : new Contas();
        }

        public Contas GetContasByEmail(string email)
        {
            var conta = GetContas().Where(x => x.Email == email).FirstOrDefault();
            return conta != null ? conta : new Contas();
        }

        public Contas GetContasByContaAgencia(int idConta, int agencia)
        {
            var conta = GetContas().Where(x => x.Conta == idConta && x.Agencia == agencia).FirstOrDefault();
            return conta != null ? conta : new Contas();
        }

        private Contas ConvertDtoToEntidade(ContaDto contaDto)
        {            
            Contas novaConta = new Contas();
            novaConta.Agencia = Agencia;
            novaConta.Conta = 0;
            novaConta.CpfCnpj = contaDto.CpfCnpj;
            novaConta.Nome = contaDto.Nome;
            novaConta.Email = contaDto.Email;
            novaConta.Saldo = contaDto.Saldo;
            novaConta.DataCriacao = DateTime.Now;
            novaConta.DataAlterado = DateTime.Now;

            return novaConta;
        }

        
    }
}
