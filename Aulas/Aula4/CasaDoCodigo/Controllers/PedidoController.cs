using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly ICadastroRepository cadastroRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            ICadastroRepository cadastroRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.cadastroRepository = cadastroRepository;
        }

        public IActionResult Carrossel()
        {
            IList<Produto> produtos = produtoRepository.GetAll();
            return View(produtos);
        }

        public IActionResult Carrinho(int produtoId)
        {
            pedidoRepository.AddItem(produtoId);
            var items = pedidoRepository.GetItems();
            return View(items);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            if (ModelState.IsValid)
            {
                var pedido = pedidoRepository.Get();
                cadastroRepository.UpdateCadastro(cadastro, pedido.Cadastro);

                return View(pedido);
            }
            return RedirectToAction("Cadastro");
        }
    }
}