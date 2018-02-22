using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(Contexto context,
            IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
        {
        }

        public List<Produto> GetProdutos()
        {
            return this._context.Produtos.ToList();
        }
    }
}