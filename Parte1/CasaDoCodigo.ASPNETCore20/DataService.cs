using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using CasaDoCodigo.Repository;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext _contexto;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProdutoRepository _produtoRepository;

        public DataService(ApplicationContext contexto,
            IHttpContextAccessor contextAccessor,
            IProdutoRepository produtoRepository)
        {
            this._contexto = contexto;
            this._contextAccessor = contextAccessor;
            this._produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            var json = string.Join("", System.IO.File.ReadAllLines(@"livros.json"));
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

            this._contexto.Database.EnsureCreated();
            if (_produtoRepository.NoProducts())
            {
                foreach (var l in livros.OrderBy(l => l.Id))
                {
                    int id = l.Id;
                    string nome = l.Name;
                    decimal preco = 69.90m;
                    _produtoRepository.AdicionarProduto(id, nome, preco);
                }

                this._contexto.SaveChanges();
            }
        }
    }

    internal class Livro
    {
        public Livro(int id, string name, string src)
        {
            this.Id = id;
            this.Name = name;
            this.Src = src;
        }

        public int Id { get; }
        public string Name { get; }
        public string Src { get; }
    }
}
