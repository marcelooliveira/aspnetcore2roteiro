﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models
{
    public class ItemPedido : BaseModel
    {
        [DataMember]
        public Pedido Pedido { get; private set; }
        [DataMember]
        public Produto Produto { get; private set; }
        [DataMember]
        public int Quantidade { get; private set; }
        [DataMember]
        public decimal PrecoUnitario { get; private set; }
        [DataMember]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public ItemPedido()
        {

        }

        public ItemPedido(Pedido pedido, Produto produto, int quantidade)
        {
            this.Pedido = pedido;
            this.Produto = produto;
            this.Quantidade = quantidade;
            this.PrecoUnitario = produto.Preco;
        }

        public void AtualizaQuantidade(int quantidade)
        {
            this.Quantidade = quantidade;
        }
    }
}
