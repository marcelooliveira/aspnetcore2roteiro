using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.ASPNETCore20.Repository
{
    public class BaseMap<T> where T: BaseModel
    {
        public BaseMap(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
        }
    }
}
