using CasaDoCodigo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Repository
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        Pedido UpdateCastro(Pedido cadastro);
    }
}