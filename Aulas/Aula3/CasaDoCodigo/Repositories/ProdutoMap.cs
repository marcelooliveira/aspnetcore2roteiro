using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoMap : BaseMap<Produto>
    {
        public ProdutoMap(EntityTypeBuilder<Produto> builder) : base(builder)
        {
            builder.Property(t => t.Codigo).IsRequired();
            builder.Property(t => t.Nome).IsRequired();
            builder.Property(t => t.Preco).IsRequired();
        }
    }
}
