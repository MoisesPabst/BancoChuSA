using BancoChu.Entidades;
using BancoChu.Entidades.Dto;
using BancoChu.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BancoChu.Services.Services
{
    public class MovimentacaoServices : IMovimentacaoServices
    {
        private readonly IMemoryCache _cache;
        private readonly IContasServices _contasServices;
        private static readonly HttpClient client = new HttpClient();
        public MovimentacaoServices(IMemoryCache cache,
            IContasServices contasServices)
        {
            _cache = cache;
            _contasServices = contasServices;
        }

        public bool MovimentarConta(MovimentacaoDto movimento)
        {
            var feriados = new List<Feriados>();
            var conta = _contasServices.GetContasByCpfCNPJ(movimento.CpfCnpj);
            var destino = GetDestino(movimento);

            if (conta == destino)
                return false;

            var ListMovimentacao = GetMovimentacao();
            var id = (ListMovimentacao.Count == 0 || ListMovimentacao.Max(x => x.Id) == 0) ? 1 : ListMovimentacao.Max(x => x.Id) + 1;

            if(_cache.Get("Movimentacao") == null)
            {
                feriados = GetFeriados().Result;
                _cache.Set("Feriados", feriados);
            } else
            {
                feriados = (List<Feriados>)_cache.Get("Feriados");
            }
            
            var dataMovimentocao = GetDataOperacao(feriados);

            var movimentacao = new Movimentacao()
            {
                Id = id,
                IdConta = conta.Conta,
                Agencia = conta.Agencia,
                Descricao = movimento.Descricao,
                Valor = movimento.Valor,
                IdContaDestino = destino != new Contas() ? destino.Conta : null,
                AgenciaDestino = destino != new Contas() ? destino.Agencia : null,
                ChavePix = movimento.ChavePix,
                Tipo = movimento.Tipo,
                DataMovimentacao = dataMovimentocao
            };

            _contasServices.MovimentaSaldo(movimentacao);

            ListMovimentacao.Add(movimentacao);
            _cache.Set("Movimentacao", ListMovimentacao);

            return true;
        }

        public List<Movimentacao> GetMovimentacao()
        {
            return _cache.Get("Movimentacao") == null ? new List<Movimentacao>() : (List<Movimentacao>)_cache.Get("Movimentacao");
        }

        public List<Movimentacao> GetMovimentacaoByAgenciaConta(int conta, int agencia)
        {
            return GetMovimentacao().Where(x => x.IdConta == conta && x.Agencia == agencia).ToList();
        }

        private DateTime GetDataOperacao(List<Feriados> feriados)
        {
            var datasFeriados = feriados.Select(x => x.Date).ToList();
            var dataMovimentacao = DateTime.Now.Date;

            if (!datasFeriados.Contains(dataMovimentacao) && dataMovimentacao.DayOfWeek != DayOfWeek.Sunday && dataMovimentacao.DayOfWeek != DayOfWeek.Saturday)
            {
                return dataMovimentacao;
            }

            do
            {
                dataMovimentacao = dataMovimentacao.AddDays(1);
            } while (datasFeriados.Contains(dataMovimentacao) || dataMovimentacao.DayOfWeek == DayOfWeek.Sunday || dataMovimentacao.DayOfWeek == DayOfWeek.Saturday);
            
            return dataMovimentacao;
        }

        private static async Task<List<Feriados>?> GetFeriados()
        {
            var feriados = new List<Feriados>();
            string url = "https://brasilapi.com.br/api/feriados/v1/" + DateTime.Now.Year;

            HttpResponseMessage resposta = await client.GetAsync(url);
            if (resposta.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                string conteudoResposta = await resposta.Content.ReadAsStringAsync();
                feriados = JsonSerializer.Deserialize<List<Feriados>>(conteudoResposta, options);
            }            

            return feriados;
        }
        private Contas GetDestino(MovimentacaoDto movimento)
        {
            var destino = new Contas();
            if (movimento.IdContaDestino.HasValue && movimento.AgenciaDestino.HasValue)
            {
                destino = _contasServices.GetContasByContaAgencia((int)movimento.IdContaDestino, (int)movimento.AgenciaDestino);
            }
            else if (movimento.ChavePix != "")
            {
                destino = _contasServices.GetContasByCpfCNPJ(movimento.ChavePix);

                if (destino == new Contas())
                    destino = _contasServices.GetContasByEmail(movimento.ChavePix);
            }

            return destino;
        }
    }
}
