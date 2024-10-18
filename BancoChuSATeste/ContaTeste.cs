using BancoChu.Entidades.Dto;
using BancoChu.Services.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace BancoChuSATeste
{
    public class ContaTeste
    {
        private IMemoryCache _cache;
                
        [SetUp]
        public void Setup()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }

        [Test]
        public void TesteCreateConta()
        {
            var servicoData = new ContasServices(_cache);

            ContaDto novaConta = new ContaDto();
            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés";
            novaConta.Email = "moises.pabst@gmail.com";
            novaConta.Saldo = 200.52M;

            servicoData.CreateConta(novaConta);
            var resultado = servicoData.GetContas();
            Assert.That(1, Is.EqualTo(resultado.Count));
        }

        [Test]
        public void TesteDuplicarConta()
        {
            var servicoData = new ContasServices(_cache);

            ContaDto novaConta = new ContaDto();
            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés";
            novaConta.Email = "moises.pabst@gmail.com";
            novaConta.Saldo = 200.52M;

            servicoData.CreateConta(novaConta);
            var retorno = servicoData.CreateConta(novaConta);

            var resultado = servicoData.GetContas();
            Assert.That(1, Is.EqualTo(resultado.Count));
            Assert.That(false, Is.EqualTo(retorno));
        }

        [Test]
        public void TesteUpdateConta()
        {
            var servicoData = new ContasServices(_cache);

            ContaDto novaConta = new ContaDto();
            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés";
            novaConta.Email = "moises.pabst@gmail.com";
            novaConta.Saldo = 200.52M;

            servicoData.CreateConta(novaConta);

            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés2";
            novaConta.Email = "moisespabst@gmail.com";
            novaConta.Saldo = 232.52M;

            servicoData.UpdateConta(novaConta);

            var resultado = servicoData.GetContas();
            Assert.That("Moisés2", Is.EqualTo(resultado.First().Nome));
            Assert.That("moisespabst@gmail.com", Is.EqualTo(resultado.First().Email));
            Assert.That(200.52M, Is.EqualTo(resultado.First().Saldo));
            Assert.That(1, Is.EqualTo(resultado.Count));
        }

        [Test]
        public void TesteDeleteConta()
        {
            var servicoData = new ContasServices(_cache);

            ContaDto novaConta = new ContaDto();
            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés";
            novaConta.Email = "moises.pabst@gmail.com";
            novaConta.Saldo = 200.52M;

            servicoData.CreateConta(novaConta);

            ContaDto novaConta2 = new ContaDto();
            novaConta2.CpfCnpj = "2";
            novaConta2.Nome = "Moisés2";
            novaConta2.Email = "moisespabst@gmail.com";
            novaConta2.Saldo = 232.52M;

            servicoData.CreateConta(novaConta2);
            servicoData.DeleteConta("1");

            var resultado = servicoData.GetContas();
            Assert.That(1, Is.EqualTo(resultado.Count));
        }

        [Test]
        public void TesteGetsConta()
        {
            var servicoData = new ContasServices(_cache);

            ContaDto novaConta = new ContaDto();
            novaConta.CpfCnpj = "1";
            novaConta.Nome = "Moisés";
            novaConta.Email = "moises.pabst@gmail.com";
            novaConta.Saldo = 200.52M;

            servicoData.CreateConta(novaConta);

            ContaDto novaConta2 = new ContaDto();
            novaConta2.CpfCnpj = "2";
            novaConta2.Nome = "Moisés2";
            novaConta2.Email = "moisespabst@gmail.com";
            novaConta2.Saldo = 232.52M;

            servicoData.CreateConta(novaConta2);

            var resultado = servicoData.GetContas();
            Assert.That(2, Is.EqualTo(resultado.Count));

            var resultadoAgenciaConta = servicoData.GetContasByContaAgencia(1, 1278);
            Assert.That("1", Is.EqualTo(resultadoAgenciaConta.CpfCnpj));

            var resultadoCpfCnpj = servicoData.GetContasByCpfCNPJ("2");
            Assert.That(1278, Is.EqualTo(resultadoCpfCnpj.Agencia));
            Assert.That(2, Is.EqualTo(resultadoCpfCnpj.Conta));

            var resultadoEmail = servicoData.GetContasByEmail("moises.pabst@gmail.com");
            Assert.That(1278, Is.EqualTo(resultadoEmail.Agencia));
            Assert.That(1, Is.EqualTo(resultadoEmail.Conta));
            Assert.That("1", Is.EqualTo(resultadoEmail.CpfCnpj));
        }
    }
}