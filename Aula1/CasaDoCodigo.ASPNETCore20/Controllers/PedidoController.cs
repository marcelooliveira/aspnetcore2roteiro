using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
using CasaDoCodigo.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IDataService _dataService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IItemPedidoRepository _itemPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        
        public PedidoController(IDataService dataService
                    , IProdutoRepository produtoRepository
                    , IItemPedidoRepository itemPedidoRepository
                    , IPedidoRepository pedidoRepository            
            )
        {
            this._dataService = dataService;
            this._produtoRepository = produtoRepository;
            this._itemPedidoRepository = itemPedidoRepository;
            this._pedidoRepository = pedidoRepository;
        }

        public IActionResult Carrossel()
        {
            List<Produto> produtos = _produtoRepository.GetProdutos();
            return View(produtos);
        }

        public IActionResult Carrinho(int? produtoId)
        {
            if (produtoId.HasValue)
            {
                _itemPedidoRepository.AddItemPedido(produtoId.Value);
            }

            CarrinhoViewModel viewModel = GetCarrinhoViewModel();

            return View(viewModel);
        }

        private CarrinhoViewModel GetCarrinhoViewModel()
        {
            List<Produto> produtos =
                this._produtoRepository.GetProdutos();

            var itensCarrinho = this._itemPedidoRepository.GetItensPedido();

            CarrinhoViewModel viewModel =
                new CarrinhoViewModel(itensCarrinho);
            return viewModel;
        }

        public IActionResult Cadastro()
        {
            var pedido = _pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }
            else
            {
                return View(pedido);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Pedido cadastro)
        {
            if (ModelState.IsValid)
            {
                var pedido = _pedidoRepository.UpdateCastro(cadastro);

                return View(pedido);
            }
            else
            {
                return RedirectToAction("Cadastro");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public UpdateItemPedidoResponse PostQuantidade([FromBody]ItemPedido input)
        {
            return _itemPedidoRepository.UpdateItemPedido(input);
        }
    }
}
