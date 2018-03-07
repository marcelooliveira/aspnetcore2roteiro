using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
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
}
