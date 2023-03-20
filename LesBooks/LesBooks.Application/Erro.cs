using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application
{
    public class Erro
    {
        public string cod { get; set; }
        public string descricao { get; set; }
        public Exception detalhes { get; set; }
    }
}
