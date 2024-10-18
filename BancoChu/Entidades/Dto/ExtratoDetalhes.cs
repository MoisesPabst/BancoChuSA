using BancoChu.Entidades.Enums;

namespace BancoChu.Entidades.Dto
{
    public class ExtratoDetalhes
    {
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Tipo { get; set; }
        public DateTime DataMovimentacao { get; set; }
    }
}
