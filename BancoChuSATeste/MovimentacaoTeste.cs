using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Entidades.Enums;
using BancoChu.Services.Interfaces;
using BancoChu.Services.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace BancoChuSATeste
{
    public class MovimentacaoTeste
    {
        private IMemoryCache _cache;
        private readonly IContasServices _contasServices;

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
                Conta = 1,
                Agencia = 1278,
            });

            mockContas.Setup(x => x.GetContasByCpfCNPJ("2")).Returns(new Contas()
            {
                Conta = 2,
                Agencia = 1278,
            });

            mockContas.Setup(x => x.GetContasByCpfCNPJ("moisespabst@gmail.com")).Returns(new Contas());

            mockContas.Setup(x => x.GetContasByEmail("moisespabst@gmail.com")).Returns(new Contas()
            {
                Conta = 2,
                Agencia = 1278,
            });

            mockContas.Setup(x => x.GetContasByContaAgencia(2, 1278)).Returns(new Contas()
            {
                Conta = 2,
                Agencia = 1278,
            });

            var servicoMovimentacao = new MovimentacaoServices(_cache, mockContas.Object);


            MovimentacaoDto movi1 = new MovimentacaoDto();
            movi1.Descricao = "Primeira";
            movi1.CpfCnpj = "1";
            movi1.Valor = 24;
            movi1.ChavePix = "2";
            movi1.Tipo = TipoMovimentacaoEnum.Entrada;

            servicoMovimentacao.MovimentarConta(movi1);

            MovimentacaoDto movi2 = new MovimentacaoDto();
            movi2.Descricao = "Segunda";
            movi2.CpfCnpj = "1";
            movi2.Valor = 29;
            movi2.ChavePix = "2";
            movi2.Tipo = TipoMovimentacaoEnum.Saida;

            servicoMovimentacao.MovimentarConta(movi2);

            MovimentacaoDto movi3 = new MovimentacaoDto();
            movi3.Descricao = "Terceira";
            movi3.CpfCnpj = "1";
            movi3.Valor = 26;
            movi3.ChavePix = "moisespabst@gmail.com";
            movi3.Tipo = TipoMovimentacaoEnum.Saida;

            servicoMovimentacao.MovimentarConta(movi3);

            MovimentacaoDto movi4 = new MovimentacaoDto();
            movi4.Descricao = "Quarta";
            movi4.CpfCnpj = "1";
            movi4.Valor = 26;
            movi4.ChavePix = "";
            movi4.IdContaDestino = 2;
            movi4.AgenciaDestino = 1278;
            movi4.Tipo = TipoMovimentacaoEnum.Saida;

            servicoMovimentacao.MovimentarConta(movi4);

            var movimentacoes = servicoMovimentacao.GetMovimentacao();
            Assert.That(4, Is.EqualTo(movimentacoes.Count));
        }
    }
}
