using BancoChu.Entidades.Dto;

namespace BancoChu.Services.Interfaces
{
    public interface IExtratoServices
    {
        public ExtratoDto GetExtratoByCpfCnpj(string cpfCnpj);
    }
}
