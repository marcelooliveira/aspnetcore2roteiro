using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly Contexto _contexto;
        private readonly IHttpContextAccessor _contextAccessor;

        public DataService(Contexto contexto,
            IHttpContextAccessor contextAccessor)
        {
            this._contexto = contexto;
            this._contextAccessor = contextAccessor;
        }

        public void InicializaDB()
        {
            var json = string.Join("", System.IO.File.ReadAllLines(@"livros.json"));
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

            this._contexto.Database.EnsureCreated();
            if (this._contexto.Produtos.Count() == 0)
            {
                foreach (var l in livros.OrderBy(l => l.Id))
                {
                    this._contexto.Produtos.Add(new Produto($"{l.Id:d3}", l.Name, 69.90m));
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
