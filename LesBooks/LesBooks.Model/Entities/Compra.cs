using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Compra : Pedido
    {
        public Compra()
        {
            this.itens = null;
        }
    }
}
