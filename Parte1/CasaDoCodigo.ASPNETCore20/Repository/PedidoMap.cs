using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
    public class PedidoMap
    {
        public PedidoMap(EntityTypeBuilder<Pedido> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.HasMany(t => t.Itens).WithOne(t => t.Pedido);
            entityBuilder.HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();
        }
    }
}
