using CasaDoCodigo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Repository
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        Pedido UpdateCadastro(Pedido cadastro);
    }
}