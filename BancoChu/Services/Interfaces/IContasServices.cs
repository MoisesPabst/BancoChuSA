using BancoChu.Entidades;
using BancoChu.Entidades.Dto;

namespace BancoChu.Services.Interfaces
{
    public interface IContasServices
    {
        public bool CreateConta(ContaDto conta);
        public bool UpdateConta(ContaDto conta);
        public bool DeleteConta(string cfpCnpj);
        public List<Contas> GetContas();
        public Contas GetContasByCpfCNPJ(string cpfCnpj);
        public Contas GetContasByEmail(string email);
        public Contas GetContasByContaAgencia(int idConta, int agencia);
    }
}
