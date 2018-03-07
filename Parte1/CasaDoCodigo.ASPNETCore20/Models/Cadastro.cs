using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class Cadastro : BaseModel
    {
        public Cadastro()
        {
        }

        public virtual Pedido Pedido { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(50, MinimumLength = 5, 
            ErrorMessage = "Nome deve ter entre 5 e 50 caracteres")]
        public string Nome { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Telefone { get; set; } = "";

        [Required]
        public string Endereco { get; set; } = "";
        public string Complemento { get; set; } = "";
        [Required]
        public string Bairro { get; set; } = "";

        [Required]
        public string Municipio { get; set; } = "";
        [Required]
        public string UF { get; set; } = "";
        [Required]
        public string CEP { get; set; } = "";
    }
}
