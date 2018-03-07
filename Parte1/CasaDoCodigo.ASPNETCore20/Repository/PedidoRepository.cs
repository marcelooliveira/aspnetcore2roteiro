using CasaDoCodigo.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CasaDoCodigo.ASPNETCore20;

namespace CasaDoCodigo.Repository
{
    public interface IPedidoRepository
    {
        Pedido GetPedido();
        Pedido GetOrCreatePedido(int pedidoId);
    }

    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private DbSet<Pedido> pedidos;
        private readonly ICadastroRepository cadastroRepository;

        public PedidoRepository(ApplicationContext context,
            ISessionManager sessionManager,
            ICadastroRepository cadastroRepository) : base(context, sessionManager)
        {
            pedidos = context.Set<Pedido>();
            this.cadastroRepository = cadastroRepository;
        }

        public Pedido GetPedido()
        {
            int? pedidoId = sessionManager.GetSessionPedidoId();

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
    }
}