using System.Collections.Generic;
using System.Linq;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;

namespace CasaDoCodigo.Repository
{
    public class ItemPedidoRepository : RepositoryBase<ItemPedido>, IItemPedidoRepository
    {
        public ItemPedidoRepository(Contexto context,
            IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
        {
        }

        public void AddItemPedido(int produtoId)
        {
            var produto =
                _context.Produtos
                .Where(p => p.Id == produtoId)
                .SingleOrDefault();

            if (produto != null)
            {
                int? pedidoId = GetSessionPedidoId();

                Pedido pedido = null;
                if (pedidoId.HasValue)
                {
                    pedido = _context.Pedidos
                        .Where(p => p.Id == pedidoId.Value)
                        .SingleOrDefault();
                }

                if (pedido == null)
                    pedido = new Pedido();

                if (!_context.ItensPedido
                    .Where(i =>
                        i.Pedido.Id == pedido.Id
                        && i.Produto.Id == produtoId)
                    .Any())
                {
                    _context.ItensPedido.Add(
                        new ItemPedido(pedido, produto, 1));

                    _context.SaveChanges();

                    SetSessionPedidoId(pedido);
                }
            }

        }

        private void SetSessionPedidoId(Pedido pedido)
        {
            _contextAccessor.HttpContext
                .Session.SetInt32("pedidoId", pedido.Id);
        }

        public List<ItemPedido> GetItensPedido()
        {
            var pedidoId = GetSessionPedidoId();
            var pedido = _context.Pedidos
                .Where(p => p.Id == pedidoId)
                .Single();

            return this._context.ItensPedido
                .Where(i => i.Pedido.Id == pedido.Id)
                .ToList();
        }

        public UpdateItemPedidoResponse UpdateItemPedido(ItemPedido itemPedido)
        {
            var itemPedidoDB =
            _context.ItensPedido
                .Where(i => i.Id == itemPedido.Id)
                .SingleOrDefault();

            if (itemPedidoDB != null)
            {
                itemPedidoDB.AtualizaQuantidade(itemPedido.Quantidade);

                if (itemPedidoDB.Quantidade == 0)
                    _context.ItensPedido.Remove(itemPedidoDB);

                _context.SaveChanges();
            }

            var itensPedido = _context.ItensPedido.ToList();

            var carrinhoViewModel = new CarrinhoViewModel(itensPedido);

            return new UpdateItemPedidoResponse(itemPedidoDB, carrinhoViewModel);

        }
    }
}
