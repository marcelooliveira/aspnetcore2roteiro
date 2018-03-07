using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repository
{
    public class RepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly ApplicationContext _context;

        public RepositoryBase(ApplicationContext context,
            IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
            this._context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
