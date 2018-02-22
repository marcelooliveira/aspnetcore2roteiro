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
        protected readonly Contexto _context;
        //protected DbSet<TEntity> _dbSet;

        public RepositoryBase(Contexto context,
            IHttpContextAccessor contextAccessor)
        {
            this._contextAccessor = contextAccessor;
            this._context = context;
            //this._dbSet = context.Set<TEntity>();
        }

        public void Dispose()
        {
            //_dbSet = null;
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        protected int? GetSessionPedidoId()
        {
            return _contextAccessor.HttpContext
                .Session.GetInt32("pedidoId");
        }
    }
}
