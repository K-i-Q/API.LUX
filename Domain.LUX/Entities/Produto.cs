using System;

namespace Domain.LUX.Entities
{
    public class Produto : Entidade
    {
        public string Descricao { get; set; }
        public DateTimeOffset DataCadastro { get; set; }
        public DateTimeOffset DataAtualizacao { get; set; }
        public decimal? Preco { get; set; }

        public Produto() { }

        public Produto(string descricao, DateTimeOffset dataCadastro, DateTimeOffset dataAtualizacao, decimal preco)
        {
            Descricao = descricao;
            DataCadastro = dataCadastro;
            DataAtualizacao = dataAtualizacao;
            Preco = preco;
        }

        public void Atualizar(string descricao, DateTimeOffset data, decimal? preco)
        {
            if (!string.IsNullOrEmpty(descricao))
                Descricao = descricao;

            if (preco != default)
                Preco = preco;

            DataAtualizacao = data;
        }

        public override bool Equals(object obj)
        {
            return obj is Produto produto &&
                   Id == produto.Id &&
                   Descricao == produto.Descricao &&
                   DataCadastro.Equals(produto.DataCadastro) &&
                   DataAtualizacao.Equals(produto.DataAtualizacao) &&
                   Preco == produto.Preco;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Descricao, DataCadastro, DataAtualizacao, Preco);
        }
    }
}
