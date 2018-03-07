using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
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
