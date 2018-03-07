using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaDoCodigo.Repository
{
    public interface ICadastroRepository
    {
        void SaveCadastro(Cadastro cadastro);
        Cadastro CreateCadastro();
        void UpdateCadastro(Cadastro origem, Cadastro destino);
    }

    public class CadastroRepository : RepositoryBase<Pedido>, ICadastroRepository
    {
        private readonly ApplicationContext context;

        public CadastroRepository(ApplicationContext context,
            IHttpContextAccessor contextAccessor) : base(context, contextAccessor)
        {
            this.context = context;
        }

        public void SaveCadastro(Cadastro cadastro)
        {
            context.Entry(cadastro).State = EntityState.Added;
            context.SaveChanges();
        }

        public Cadastro CreateCadastro()
        {
            Cadastro cadastro = new Cadastro();
            _context.SaveChanges();
            return cadastro;
        }

        public void UpdateCadastro(Cadastro origem, Cadastro destino)
        {
            destino.Nome = origem.Nome;
            destino.Email = origem.Email;
            destino.Telefone = origem.Telefone;
            destino.Endereco = origem.Endereco;
            destino.Complemento = origem.Complemento;
            destino.Bairro = origem.Bairro;
            destino.Municipio = origem.Municipio;
            destino.UF = origem.UF;
            destino.CEP = origem.CEP;
            _context.SaveChanges();
        }
    }
}