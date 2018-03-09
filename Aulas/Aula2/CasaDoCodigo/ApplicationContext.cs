using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new ProdutoMap(modelBuilder.Entity<Produto>());
            new CadastroMap(modelBuilder.Entity<Cadastro>());
            new PedidoMap(modelBuilder.Entity<Pedido>());
            new ItemPedidoMap(modelBuilder.Entity<ItemPedido>());
        }
    }
}
