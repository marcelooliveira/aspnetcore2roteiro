using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class BaseRepository
    {
        protected readonly ApplicationContext contexto;
        protected readonly ISessionManager sessionManager;

        public BaseRepository(ApplicationContext contexto
            , ISessionManager sessionManager)
        {
            this.contexto = contexto;
            this.sessionManager = sessionManager;
        }
    }
}
