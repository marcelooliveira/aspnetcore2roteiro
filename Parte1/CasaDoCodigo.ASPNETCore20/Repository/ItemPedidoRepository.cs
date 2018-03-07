using System.Collections.Generic;
using System.Linq;
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
        void AddItemPedido(int produtoId);
    }

    public class ItemPedidoRepository : RepositoryBase<ItemPedido>, IItemPedidoRepository
    {
        private IProdutoRepository produtoRepository;
        private IPedidoRepository pedidoRepository;
        private DbSet<ItemPedido> itensPedido;

        public ItemPedidoRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor,
            IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository) : base(context, contextAccessor)
        {
            this.itensPedido = context.Set<ItemPedido>();
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
        }

        public void AddItemPedido(int produtoId)
        {
            Produto produto = produtoRepository.GetProduto(produtoId);

            if (produto != null)
            {
                int pedidoId = pedidoRepository.GetSessionPedidoId() ?? 0;

                Pedido pedido = null;
                pedido = pedidoRepository.GetOrCreatePedido(pedidoId);

                if (!itensPedido
                    .Where(i =>
                        i.Pedido.Id == pedido.Id
                        && i.Produto.Id == produtoId)
                    .Any())
                {
                    itensPedido.Add(
                        new ItemPedido(pedido, produto, 1));

                    _context.SaveChanges();

                    pedidoRepository.SetSessionPedidoId(pedido);
                }
            }
        }

        public List<ItemPedido> GetItensPedido()
        {
            var pedidoId = pedidoRepository.GetSessionPedidoId();
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

                _context.SaveChanges();
            }

            var carrinhoViewModel = new CarrinhoViewModel(itensPedido.ToList());

            return new UpdateItemPedidoResponse(itemPedidoDB, carrinhoViewModel);

        }
    }
}
