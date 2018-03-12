using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ItemPedidoMap : BaseMap<ItemPedido>
    {
        public ItemPedidoMap(EntityTypeBuilder<ItemPedido> builder) : base(builder)
        {
            builder.HasOne(t => t.Pedido);
            builder.HasOne(t => t.Produto);
            builder.Property(t => t.Quantidade).IsRequired();
            builder.Property(t => t.PrecoUnitario).IsRequired();
        }
    }
}
