﻿using BancoChu.Entidades.Enums;

namespace BancoChu.Entidades
{
    public class Movimentacao
    {
        public int Id {  get; set; }
        public int IdConta { get; set; }
        public int Agencia { get; set; }
        public string? Descricao { get; set; }
        public decimal Valor { get; set; }
        public int? IdContaDestino { get; set; }
        public int? AgenciaDestino { get; set; }
        public string? ChavePix { get; set; }

        public TipoMovimentacaoEnum Tipo { get; set; }
        public DateTime DataMovimentacao { get; set; }
    }
}
