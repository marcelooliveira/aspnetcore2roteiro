using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
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
}
