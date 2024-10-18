using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Entidades.Enums;
using BancoChu.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BancoChu.Services.Services
{
    public class ExtratoServices : IExtratoServices
    {
        private readonly IMemoryCache _cache;
        private readonly IMovimentacaoServices _movimentacaoServices;
        private readonly IContasServices _contasServices;

        public ExtratoServices(IMemoryCache cache,
            IMovimentacaoServices movimentacaoServices,
            IContasServices contasServices)
        {
            _cache = cache;
            _movimentacaoServices = movimentacaoServices;
            _contasServices = contasServices;
        }

        public ExtratoDto GetExtratoByCpfCnpj(string cpfCnpj)
        {
            var conta = _contasServices.GetContasByCpfCNPJ(cpfCnpj);
            var movimentacoes = _movimentacaoServices.GetMovimentacaoByAgenciaConta(conta.Conta, conta.Agencia);

            var extrato = new ExtratoDto()
            {
                CpfCnpj = conta.CpfCnpj,
                Nome = conta.Nome,
                Conta = conta.Conta,
                Agencia = conta.Agencia,
                Saldo = conta.Saldo,
                Detalhes = MapperMovimentacaoToDto(movimentacoes)
            };

            return extrato;
        }

        public List<ExtratoDetalhes> MapperMovimentacaoToDto(List<Movimentacao> movimentacao)
        {
            List<ExtratoDetalhes> moviReturn = new List<ExtratoDetalhes>();

            movimentacao.ForEach(x =>
            {
                moviReturn.Add(new ExtratoDetalhes()
                {
                    Descricao = x.Descricao,
                    Valor = x.Tipo == TipoMovimentacaoEnum.Saida ? x.Valor*(-1) : x.Valor,
                    Tipo = x.Tipo == TipoMovimentacaoEnum.Saida ? "Saida" : "Entrada",
                    DataMovimentacao = x.DataMovimentacao
                });
            });

            return moviReturn;
        }
    }
}
