using BancoChu.Entidades.Enums;

namespace BancoChu.Entidades
{
    public class Movimentacao
    {
        public int Id {  get; set; }
        public string IdConta { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public int? IdContaDestino { get; set; }

        public TipoMovimentacaoEnum Tipo { get; set; }
    }
}
