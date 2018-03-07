using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaDoCodigo.Repository
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        Pedido GetOrCreatePedido(int pedidoId);
        int? GetSessionPedidoId();
        void SetSessionPedidoId(Pedido pedido);
    }

    public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
    {
        private DbSet<Pedido> pedidos;
        private readonly ICadastroRepository cadastroRepository;

        public PedidoRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor,
            ICadastroRepository cadastroRepository) : base(context, contextAccessor)
        {
            pedidos = context.Set<Pedido>();
            this.cadastroRepository = cadastroRepository;
        }

        public Pedido GetPedido()
        {
            int? pedidoId = GetSessionPedidoId();

            return pedidos
                    .Include(p => p.Itens)
                        .ThenInclude(p => p.Produto)
                    .Include(p => p.Cadastro)
                    .Where(p => p.Id == pedidoId)
                    .SingleOrDefault();
        }

        public Pedido GetOrCreatePedido(int pedidoId)
        {
            Pedido pedido = pedidos
            .Where(p => p.Id == pedidoId)
            .SingleOrDefault();

            if (pedido == null)
            {
                Cadastro cadastro = cadastroRepository.CreateCadastro();
                pedido = new Pedido(cadastro);
            }

            return pedido;
        }

        public int? GetSessionPedidoId()
        {
            return _contextAccessor.HttpContext
                .Session.GetInt32("pedidoId");
        }

        public void SetSessionPedidoId(Pedido pedido)
        {
            _contextAccessor.HttpContext
                .Session.SetInt32("pedidoId", pedido.Id);
        }

    }
}