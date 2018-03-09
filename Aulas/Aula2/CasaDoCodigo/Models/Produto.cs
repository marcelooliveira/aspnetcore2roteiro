using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Produto : BaseEntity
    {
        public Produto(string id, string nome, decimal preco)
        {
            this.Codigo = $"{id:d3}";
            this.Nome = nome;
            this.Preco = preco;
        }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
    }
}
