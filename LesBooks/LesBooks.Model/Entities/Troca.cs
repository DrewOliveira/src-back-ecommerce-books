using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Troca : Pedido
    {
        public Compra compra { get; set; }
        public Troca()
        {
            compra.itens = null;
        }
    }
}
