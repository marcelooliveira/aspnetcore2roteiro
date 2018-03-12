using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public interface ISessionManager
    {
        int? GetSessionPedidoId();
        void SetSessionPedidoId(int pedidoId);
    }

    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor contextAccessor;

        public SessionManager(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public int? GetSessionPedidoId()
        {
            return contextAccessor.HttpContext
                .Session.GetInt32("pedidoId");
        }

        public void SetSessionPedidoId(int pedidoId)
        {
            contextAccessor.HttpContext
                .Session.SetInt32("pedidoId", pedidoId);
        }
    }
}
