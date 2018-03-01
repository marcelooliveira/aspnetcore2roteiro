﻿using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CasaDoCodigo
{
    public class DataService: IDataService
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
            this._contexto.Database.EnsureCreated();
            if (this._contexto.Produtos.Count() == 0)
            {
                List<Produto> produtos = new List<Produto>
                {
                    new Produto("Sleep not found", 59.90m),
                    new Produto("May the code be with you", 59.90m),
                    new Produto("Rollback", 59.90m),
                    new Produto("REST", 69.90m),
                    new Produto("Design Patterns com Java", 69.90m),
                    new Produto("Vire o jogo com Spring Framework", 69.90m),
                    new Produto("Test-Driven Development", 69.90m),
                    new Produto("iOS: Programe para iPhone e iPad", 69.90m),
                    new Produto("Desenvolvimento de Jogos para Android", 69.90m)
                };

                foreach (var produto in produtos)
                {
                    this._contexto.Produtos
                        .Add(produto);

                    //this._contexto.ItensPedido
                    //    .Add(new ItemPedido(produto, 1));
                }

                this._contexto.SaveChanges();
            }
        }
    }
}