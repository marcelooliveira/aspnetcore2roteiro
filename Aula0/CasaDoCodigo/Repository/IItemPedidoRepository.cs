using System.Collections.Generic;
using CasaDoCodigo.Models;

namespace CasaDoCodigo.Repository
{
    public interface IItemPedidoRepository
    {
        List<ItemPedido> GetItensPedido();
        UpdateItemPedidoResponse UpdateItemPedido(ItemPedido itemPedido);
        void AddItemPedido(int produtoId);
    }
}
