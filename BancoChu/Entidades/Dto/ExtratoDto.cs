namespace BancoChu.Entidades.Dto
{
    public class ExtratoDto
    {
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public int Conta { get; set; }
        public int Agencia { get; set; }
        public decimal Saldo { get; set; }
        public List<ExtratoDetalhes> Detalhes { get; set; }
    }
}
