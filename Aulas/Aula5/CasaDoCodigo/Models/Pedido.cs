using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Pedido : BaseModel
    {
        public Pedido()
        {
        }

        public Pedido(Cadastro cadastro)
        {
            Cadastro = cadastro;
        }

        [DataMember]
        public List<ItemPedido> Itens { get; private set; } = new List<ItemPedido>();
        [DataMember]
        public virtual Cadastro Cadastro { get; private set; }
    }
}
