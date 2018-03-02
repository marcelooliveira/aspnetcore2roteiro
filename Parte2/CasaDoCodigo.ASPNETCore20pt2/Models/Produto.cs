using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Produto : BaseModel
    {
        [DataMember]
        public string Codigo { get; private set; }
        [DataMember]
        public string Nome { get; private set; }
        [DataMember]
        public decimal Preco { get; private set; }

        public Produto()
        {

        }

        public Produto(int id, string codigo, string nome, decimal preco)
            : this(codigo, nome, preco)
        {
            this.Id = id;
        }

        public Produto(string codigo, string nome, decimal preco)
        {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Preco = preco;
        }
    }
}
