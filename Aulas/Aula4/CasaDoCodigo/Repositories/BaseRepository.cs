using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class BaseRepository
    {
        protected readonly ApplicationContext contexto;

        public BaseRepository(ApplicationContext contexto)
        {
            this.contexto = contexto;
        }
    }
}
