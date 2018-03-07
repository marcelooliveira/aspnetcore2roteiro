using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repository
{
    public interface IProdutoRepository
    {
        List<Produto> GetProdutos();
        Produto GetProduto(int produtoId);
        bool NoProducts();
        void AdicionarProduto(int id, string nome, decimal preco);
    }

    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        private DbSet<Produto> produtos;

        public ProdutoRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
        {
            produtos = context.Set<Produto>();
        }

        public List<Produto> GetProdutos()
        {
            return produtos.ToList();
        }

        public Produto GetProduto(int produtoId)
        {
            return produtos
                .Where(p => p.Id == produtoId)
                .SingleOrDefault();
        }

        public bool NoProducts()
        {
            return produtos.Count() == 0;
        }

        public void AdicionarProduto(int id, string nome, decimal preco)
        {
            produtos.Add(new Produto($"{id:d3}", nome, preco));
        }
    }
}