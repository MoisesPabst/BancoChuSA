using BancoChu.Entidades.Enums;

namespace BancoChu.Entidades.Dto
{
    public class MovimentacaoDto
    {
        public string CpfCnpj { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public int? IdContaDestino { get; set; }
        public int? AgenciaDestino { get; set; }
        public string? ChavePix { get; set; }

        public TipoMovimentacaoEnum Tipo { get; set; }
    }
}
