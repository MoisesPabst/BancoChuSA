using BancoChu.Entidades.Dto;

namespace BancoChu.Services.Interfaces
{
    public interface IMovimentacaoServices
    {
        public bool MovimentarConta(MovimentacaoDto movimentacao);
    }
}
