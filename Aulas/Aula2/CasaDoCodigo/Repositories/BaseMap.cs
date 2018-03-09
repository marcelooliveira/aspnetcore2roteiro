using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class BaseMap<T> where T: BaseEntity
    {
        public BaseMap(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}
