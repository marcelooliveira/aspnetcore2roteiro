using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class PedidoMap : BaseMap<Pedido>
    {
        public PedidoMap(EntityTypeBuilder<Pedido> builder) : base(builder)
        {
            builder.HasMany(t => t.Items).WithOne(t => t.Pedido);
            builder.HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();
        }
    }
}
