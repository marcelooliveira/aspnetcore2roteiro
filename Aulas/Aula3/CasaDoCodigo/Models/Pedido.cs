using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Pedido : BaseEntity
    {
        public Cadastro Cadastro { get; private set; }
        public List<ItemPedido> Items { get; private set; } = new List<ItemPedido>();
    }
}
