using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Entidades.Enums;
using BancoChu.Services.Interfaces;
using BancoChu.Services.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Text.Json;

namespace BancoChuSATeste
{
    public class ExtratoTeste
    {
        private IMemoryCache _cache;

        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        [Test]
        public void TesteMovimentacao()
        {
            var mockContas = new Mock<IContasServices>();
            mockContas.Setup(x => x.GetContasByCpfCNPJ("1")).Returns(new Contas()
            {
                CpfCnpj = "1",
                Conta = 1,
                Agencia = 1278,
                Nome = "Moises",
                Saldo = 120
            });

            List<Movimentacao> listMovimentacao = new List<Movimentacao>();
            Movimentacao movi1 = new Movimentacao()
            {
                Descricao = "123",
                Tipo = TipoMovimentacaoEnum.Saida,
                Valor = 100,
                DataMovimentacao = DateTime.Now.Date
            };
            Movimentacao movi2 = new Movimentacao()
            {
                Descricao = "1234",
                Tipo = TipoMovimentacaoEnum.Entrada,
                Valor = 90,
                DataMovimentacao = DateTime.Now.Date
            };
            Movimentacao movi3 = new Movimentacao()
            {
                Descricao = "1235",
                Tipo = TipoMovimentacaoEnum.Saida,
                Valor = 80,
                DataMovimentacao = DateTime.Now.Date
            };

            listMovimentacao.Add(movi1);
            listMovimentacao.Add(movi2);
            listMovimentacao.Add(movi3);
            var mockMovimentacao = new Mock<IMovimentacaoServices>();
            mockMovimentacao.Setup(x => x.GetMovimentacaoByAgenciaConta(1, 1278)).Returns(listMovimentacao);

            List<ExtratoDetalhes> detalhes = new List<ExtratoDetalhes>();
            ExtratoDetalhes detalhe1 = new ExtratoDetalhes()
            {
                Descricao = "123",
                Valor = -100,
                Tipo = "Saida",
                DataMovimentacao = DateTime.Now.Date
            };
            ExtratoDetalhes detalhe2 = new ExtratoDetalhes()
            {
                Descricao = "1234",
                Valor = 90,
                Tipo = "Entrada",
                DataMovimentacao = DateTime.Now.Date
            };
            ExtratoDetalhes detalhe3 = new ExtratoDetalhes()
            {
                Descricao = "1235",
                Valor = -80,
                Tipo = "Saida",
                DataMovimentacao = DateTime.Now.Date
            };

            detalhes.Add(detalhe1);
            detalhes.Add(detalhe2);
            detalhes.Add(detalhe3);


            var extrato = new ExtratoDto()
            {
                CpfCnpj = "1",
                Nome = "Moises",
                Conta = 1,
                Agencia = 1278,
                Saldo = 120,
                Detalhes = detalhes
            };

            var servicoExtrato = new ExtratoServices(_cache, mockMovimentacao.Object, mockContas.Object);
            var retornoExtrato = servicoExtrato.GetExtratoByCpfCnpj("1");

            Assert.That(extrato.Detalhes.Count, Is.EqualTo(retornoExtrato.Detalhes.Count));
            Assert.That(extrato.CpfCnpj, Is.EqualTo(retornoExtrato.CpfCnpj));
            Assert.That(extrato.Saldo, Is.EqualTo(retornoExtrato.Saldo));
        }
    }
}
