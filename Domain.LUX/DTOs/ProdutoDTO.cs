using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.LUX.DTOs
{
    public class ProdutoDTO
    {
        public string Descricao { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public DateTimeOffset DataAtualizacao { get; set; }
        public decimal? Preco { get; set; }
    }
}
