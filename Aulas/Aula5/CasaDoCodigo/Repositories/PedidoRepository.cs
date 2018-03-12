using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IPedidoRepository
    {
        ItemPedido AddItem(int produtoId);
        IList<ItemPedido> GetItems();
        Pedido Get();
    }

    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly DbSet<Pedido> pedidos;
        private readonly IProdutoRepository produtoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoRepository(ApplicationContext contexto
            , ISessionManager sessionManager
            , IHttpContextAccessor contextAccessor
            , IProdutoRepository produtoRepository
            , IItemPedidoRepository itemPedidoRepository) : base(contexto, sessionManager)
        {
            this.contextAccessor = contextAccessor;
            this.produtoRepository = produtoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
            this.pedidos = contexto.Set<Pedido>();
        }

        public ItemPedido AddItem(int produtoId)
        {
            var produto = produtoRepository.Get(produtoId);

            Pedido pedido = CreateOrGet();

            ItemPedido itemPedido = itemPedidoRepository.Get(pedido.Id, produtoId);
            if (itemPedido == null)
            {
                itemPedido =
                    new ItemPedido(pedido, produto, 1);

                pedido.Itens.Add(itemPedido);

                contexto.SaveChanges();
            }

            return itemPedido;
        }

        public IList<ItemPedido> GetItems()
        {
            return itemPedidoRepository.GetAll();
        }

        private Pedido CreateOrGet()
        {
            if (sessionManager.GetSessionPedidoId() is int pedidoId)
                return Get();

            return Create();
        }

        private Pedido Create()
        {
            var cadastro = new Cadastro();
            var pedido = new Pedido(cadastro);
            pedidos.Add(pedido);
            contexto.SaveChanges();
            sessionManager.SetSessionPedidoId(pedido.Id);
            return pedido;
        }

        public Pedido Get()
        {
            int? pedidoId = sessionManager.GetSessionPedidoId();

            return pedidos
                    .Include(p => p.Itens)
                        .ThenInclude(p => p.Produto)
                    .Include(p => p.Cadastro)
                    .Where(p => p.Id == pedidoId)
                    .SingleOrDefault();
        }
    }
}
