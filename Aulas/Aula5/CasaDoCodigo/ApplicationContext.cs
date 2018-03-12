using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

    public class BaseMap<T> where T : BaseModel
    {
        public BaseMap(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }

    public class CadastroMap : BaseMap<Cadastro>
    {
        public CadastroMap(EntityTypeBuilder<Cadastro> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.HasOne(t => t.Pedido);
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Email).IsRequired();
            entityBuilder.Property(t => t.Telefone).IsRequired();
            entityBuilder.Property(t => t.Endereco).IsRequired();
            entityBuilder.Property(t => t.Complemento).IsRequired();
            entityBuilder.Property(t => t.Bairro).IsRequired();
            entityBuilder.Property(t => t.Municipio).IsRequired();
            entityBuilder.Property(t => t.UF).IsRequired();
            entityBuilder.Property(t => t.CEP).IsRequired();
        }
    }

    public class ItemPedidoMap : BaseMap<ItemPedido>
    {
        public ItemPedidoMap(EntityTypeBuilder<ItemPedido> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.HasOne(t => t.Pedido);
            entityBuilder.HasOne(t => t.Produto);
            entityBuilder.Property(t => t.Quantidade).IsRequired();
            entityBuilder.Property(t => t.PrecoUnitario).IsRequired();
        }
    }

    public class PedidoMap : BaseMap<Pedido>
    {
        public PedidoMap(EntityTypeBuilder<Pedido> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.HasMany(t => t.Itens).WithOne(t => t.Pedido);
            entityBuilder.HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();
        }
    }

    public class ProdutoMap : BaseMap<Produto>
    {
        public ProdutoMap(EntityTypeBuilder<Produto> entityBuilder) : base(entityBuilder)
        {
            entityBuilder.Property(t => t.Codigo).IsRequired();
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Preco).IsRequired();
        }
    }
}
