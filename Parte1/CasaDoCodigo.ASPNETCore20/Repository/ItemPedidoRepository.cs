using System.Collections.Generic;
using System.Linq;
using CasaDoCodigo.ASPNETCore20;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo.Repository
{
    public interface IItemPedidoRepository
    {
        List<ItemPedido> GetItensPedido();
        UpdateItemPedidoResponse UpdateItemPedido(ItemPedido itemPedido);
        ItemPedido AddItemPedido(int produtoId);
    }

    public class ItemPedidoRepository : BaseRepository<ItemPedido>, IItemPedidoRepository
    {
        private IProdutoRepository produtoRepository;
        private IPedidoRepository pedidoRepository;
        private DbSet<ItemPedido> itensPedido;

        public ItemPedidoRepository(ApplicationContext context,
            ISessionManager sessionManager,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository) : base(context, sessionManager)
        {
            this.itensPedido = context.Set<ItemPedido>();
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
        }

        public ItemPedido AddItemPedido(int produtoId)
        {
            ItemPedido itemPedido = null;

            Produto produto = produtoRepository.GetProduto(produtoId);

            if (produto != null)
            {
                int pedidoId = sessionManager.GetSessionPedidoId() ?? 0;

                Pedido pedido = null;
                pedido = pedidoRepository.GetOrCreatePedido(pedidoId);

                itemPedido = itensPedido
                    .Include(i => i.Pedido)
                    .Where(i =>
                        i.Pedido.Id == pedido.Id
                        && i.Produto.Id == produtoId)
                    .SingleOrDefault();

                if (itemPedido == null)
                {
                    itemPedido = new ItemPedido(pedido, produto, 1);
                    itensPedido.Add(itemPedido);

                    context.SaveChanges();
                }
            }
            return itemPedido;
        }

        public List<ItemPedido> GetItensPedido()
        {
            var pedidoId = sessionManager.GetSessionPedidoId();
            var pedido = pedidoRepository.GetOrCreatePedido(pedidoId.Value);

            return this.itensPedido
                .Where(i => i.Pedido.Id == pedido.Id)
                .ToList();
        }

        public UpdateItemPedidoResponse UpdateItemPedido(ItemPedido itemPedido)
        {
            var itemPedidoDB =
            itensPedido
                .Where(i => i.Id == itemPedido.Id)
                .SingleOrDefault();

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);

                if (itemPedidoDB.Quantidade == 0)
                    itensPedido.Remove(itemPedidoDB);

                context.SaveChanges();
            }

            var carrinhoViewModel = new CarrinhoViewModel(itensPedido.ToList());

            return new UpdateItemPedidoResponse(itemPedidoDB, carrinhoViewModel);

        }
    }
}
