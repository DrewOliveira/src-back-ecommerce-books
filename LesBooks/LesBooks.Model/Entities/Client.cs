using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Model.Entities
{
    public class Client
    {
        public int id { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public string cpf { get; set; }
        public DateTime birth { get; set; }
        public User user{ get; set; }
        public Phone phone { get; set; }
        public Ranking ranking { get; set; }
        public List<Card> card { get; set; }
        public List<Adress> adresses { get; set; }

    }
}
