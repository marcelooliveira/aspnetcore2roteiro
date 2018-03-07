using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
    public class ItemPedidoMap
    {
        public ItemPedidoMap(EntityTypeBuilder<ItemPedido> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.HasOne(t => t.Pedido);
            entityBuilder.HasOne(t => t.Produto);
            entityBuilder.Property(t => t.Quantidade).IsRequired();
            entityBuilder.Property(t => t.PrecoUnitario).IsRequired();
        }
    }
}
