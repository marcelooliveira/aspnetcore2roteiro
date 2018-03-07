using CasaDoCodigo.ASPNETCore20.Repository;
using CasaDoCodigo.Models;
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
            new PedidoMap(modelBuilder.Entity<Pedido>());
            new ItemPedidoMap(modelBuilder.Entity<ItemPedido>());
            new CadastroMap(modelBuilder.Entity<Cadastro>());
        }
    }
}
