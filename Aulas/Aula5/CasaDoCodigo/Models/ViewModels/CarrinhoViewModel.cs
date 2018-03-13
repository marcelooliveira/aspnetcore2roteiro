using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class CarrinhoViewModel
    {
        [DataMember]
        public List<ItemPedido> Itens { get; private set; }
        [DataMember]
        public decimal Total {
            get
            {
                return Itens.Sum(i => i.Subtotal);
            }
        }

        public CarrinhoViewModel(List<ItemPedido> itens)
        {
            this.Itens = itens;
        }
    }
}
