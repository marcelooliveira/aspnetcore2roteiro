using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly ICadastroRepository cadastroRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;

        public PedidoController(IProdutoRepository produtoRepository,
            IPedidoRepository pedidoRepository,
            ICadastroRepository cadastroRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            this.produtoRepository = produtoRepository;
            this.pedidoRepository = pedidoRepository;
            this.cadastroRepository = cadastroRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public IActionResult Carrossel()
        {
            IList<Produto> produtos = produtoRepository.GetAll();
            return View(produtos);
        }

        public IActionResult Carrinho(int produtoId)
        {
            if (produtoId > 0)
            {
                pedidoRepository.AddItem(produtoId);
            }
            CarrinhoViewModel viewModel = GetCarrinhoViewModel();
            return View(viewModel);
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.Get();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }
            return View(pedido.Cadastro);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateItemPedidoResponse PostQuantidade([FromBody]ItemPedido input)
        {
            return itemPedidoRepository.UpdateItemPedido(input);
        }

        private CarrinhoViewModel GetCarrinhoViewModel()
        {
            var itensCarrinho = this.itemPedidoRepository.GetAll().ToList();

            CarrinhoViewModel viewModel =
                new CarrinhoViewModel(itensCarrinho);
            return viewModel;
        }
    }
}