﻿using CasaDoCodigo.Models;
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
        public ItemPedidoRepository(ApplicationContext contexto
            , ISessionManager sessionManager) : base(contexto, sessionManager)
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
            if (sessionManager.GetSessionPedidoId() is int pedidoId)
            {
                return 
                    itensPedido
                    .Include(i => i.Produto)
                    .Include(i => i.Pedido)
                    .Where(i => i.Pedido.Id == pedidoId)
                    .ToList();
            }

            throw new ApplicationException("Pedido Id não pode ser nulo");
        }

        
    }
}
