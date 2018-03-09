using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IItemPedidoRepository
    {
        IList<ItemPedido> GetAll();
        ItemPedido Get(int pedidoId, int produtoId);
    }

    public class ItemPedidoRepository : BaseRepository, IItemPedidoRepository
    {
        private readonly DbSet<ItemPedido> itensPedido;
        public ItemPedidoRepository(ApplicationContext contexto) : base(contexto)
        {
            this.itensPedido = contexto.Set<ItemPedido>();
        }

        public ItemPedido Get(int pedidoId, int produtoId)
        {
            return itensPedido
                .Include(i => i.Pedido)
                .Include(i => i.Produto)
                .Where(i =>
                    i.Pedido.Id == pedidoId
                    && i.Produto.Id == produtoId)
                .SingleOrDefault();
        }

        public IList<ItemPedido> GetAll()
        {
            return 
                itensPedido
                .Include(i => i.Produto)
                .ToList();
        }

        
    }
}
