using BancoChu.Entidades;
using BancoChu.Entidades.Dto;

namespace BancoChu.Services.Interfaces
{
    public interface IContasServices
    {
        public bool CreateConta(ContaDto conta);
        public bool UpdateConta(ContaDto conta);
        public bool DeleteConta(string CfpCnpj);
        public List<Contas> GetContas();
        public Contas GetContasByCpfCNPJ(string CpfCnpj);
    }
}
