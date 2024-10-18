using BancoChu.Entidades;
using BancoChu.Entidades.Dto;

namespace BancoChu.Services.Interfaces
{
    public interface IMovimentacaoServices
    {
        public bool MovimentarConta(MovimentacaoDto movimentacao);
        public List<Movimentacao> GetMovimentacao();
        public List<Movimentacao> GetMovimentacaoByAgenciaConta(int conta, int agencia);
    }
}
