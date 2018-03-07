using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
    public class ProdutoMap
    {
        public ProdutoMap(EntityTypeBuilder<Produto> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Codigo).IsRequired();
            entityBuilder.Property(t => t.Nome).IsRequired();
            entityBuilder.Property(t => t.Preco).IsRequired();
        }
    }
}
