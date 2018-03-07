using CasaDoCodigo.ASPNETCore20;
using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repository
{
    public class BaseRepository<TEntity> : IDisposable where TEntity : class
    {
        protected readonly ApplicationContext context;
        protected readonly ISessionManager sessionManager;

        public BaseRepository(ApplicationContext context, ISessionManager sessionManager)
        {
            this.sessionManager = sessionManager;
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
