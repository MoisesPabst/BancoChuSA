namespace BancoChu.Entidades
{
    public class Contas
    {
        public int Conta { get; set; }
        public int Agencia { get; set; }
        public string CpfCnpj { get; set; }
        public string Nome { get; set; }
        public double Saldo { get; set; }
        public string Email { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAlterado { get; set; }
    }
}
