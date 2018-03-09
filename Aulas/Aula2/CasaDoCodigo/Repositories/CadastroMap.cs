using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class CadastroMap : BaseMap<Cadastro>
    {
        public CadastroMap(EntityTypeBuilder<Cadastro> builder) : base(builder)
        {
            builder.HasOne(t => t.Pedido);
            builder.Property(t => t.Nome).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.Telefone).IsRequired();
            builder.Property(t => t.Endereco).IsRequired();
            builder.Property(t => t.Complemento).IsRequired();
            builder.Property(t => t.Bairro).IsRequired();
            builder.Property(t => t.Municipio).IsRequired();
            builder.Property(t => t.UF).IsRequired();
            builder.Property(t => t.CEP).IsRequired();
        }
    }
}
