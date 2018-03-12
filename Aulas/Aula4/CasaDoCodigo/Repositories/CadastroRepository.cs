using CasaDoCodigo.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public interface ICadastroRepository
    {
        void SaveCadastro(Cadastro cadastro);
        Cadastro CreateCadastro();
        void UpdateCadastro(Cadastro origem, Cadastro destino);
    }

    public class CadastroRepository : BaseRepository, ICadastroRepository
    {
        public CadastroRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public void SaveCadastro(Cadastro cadastro)
        {
            contexto.Entry(cadastro).State = EntityState.Added;
            contexto.SaveChanges();
        }

        public Cadastro CreateCadastro()
        {
            Cadastro cadastro = new Cadastro();
            base.contexto.SaveChanges();
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
            base.contexto.SaveChanges();
        }
    }
}