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
    }

    public class PedidoRepository : BaseRepository, IPedidoRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly DbSet<Pedido> pedidos;
        private readonly IProdutoRepository produtoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoRepository(ApplicationContext contexto
            , IHttpContextAccessor contextAccessor
            , IProdutoRepository produtoRepository
            , IItemPedidoRepository itemPedidoRepository) : base(contexto)
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
                    new ItemPedido(pedido, produto, 1, produto.Preco);

                pedido.Items.Add(itemPedido);

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
            if (GetSessionPedidoId() is int pedidoId)
                return pedidos.Where(p => p.Id == pedidoId).SingleOrDefault();

            return Create();
        }

        private Pedido Create()
        {
            var pedido = new Pedido();
            pedidos.Add(pedido);
            contexto.SaveChanges();
            SetSessionPedidoId(pedido.Id);
            return pedido;
        }

        private int? GetSessionPedidoId()
        {
            return contextAccessor.HttpContext
                .Session.GetInt32("pedidoId");
        }

        private void SetSessionPedidoId(int pedidoId)
        {
            contextAccessor.HttpContext
                .Session.SetInt32("pedidoId", pedidoId);
        }
    }
}
