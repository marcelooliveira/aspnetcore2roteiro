using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface IProdutoRepository
    {
        bool NenhumProduto();
        void Add(List<Livro> livros);
    }

    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationContext contexto;
        private readonly DbSet<Produto> produtos;

        public ProdutoRepository(ApplicationContext contexto)
        {
            this.contexto = contexto;
            this.produtos = contexto.Set<Produto>();
        }

        public void Add(List<Livro> livros)
        {
            foreach (var livro in livros)
            {
                produtos.Add(new Produto($"{livro.Id:d3}", livro.Name, 49.90M));
            }
            contexto.SaveChanges();
        }

        public bool NenhumProduto()
        {
            return produtos.Count() == 0;
        }
    }

    public class Livro
    {
        public Livro(int id, string name, string src)
        {
            Id = id;
            Name = name;
            Src = src;
        }

        public int Id { get; }
        public string Name { get; }
        public string Src { get; }
    }
}
