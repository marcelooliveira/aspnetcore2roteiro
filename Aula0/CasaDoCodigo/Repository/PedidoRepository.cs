using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaDoCodigo.Repository
{
    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        public PedidoRepository(Contexto context,
            IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
        {
        }

        public Pedido GetPedido()
        {
            int? pedidoId = GetSessionPedidoId();

            return _context.Pedidos
                        .Include(p => p.Itens)
                            .ThenInclude(p => p.Produto)
                        .Where(p => p.Id == pedidoId)
                        .SingleOrDefault();
        }

        public Pedido UpdateCastro(Pedido cadastro)
        {
            var pedido = GetPedido();
            pedido.UpdateCadastro(cadastro);
            _context.SaveChanges();
            return pedido;
        }

    }
}