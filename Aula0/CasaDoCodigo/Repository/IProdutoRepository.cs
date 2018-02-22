using CasaDoCodigo.Models;
using System.Collections.Generic;

namespace CasaDoCodigo.Repository
{
    public interface IProdutoRepository
    {
        List<Produto> GetProdutos();
    }
}