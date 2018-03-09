using CasaDoCodigo.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            this.contexto.Database.EnsureCreated();
            if (produtoRepository.NenhumProduto())
            {
                var json = System.IO.File.ReadAllText(@"livros.json");
                var livros = JsonConvert.DeserializeObject<List<Livro>>(json);

                produtoRepository.Add(livros);
            }
        }
    }
}
