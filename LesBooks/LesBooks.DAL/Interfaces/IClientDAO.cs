using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public interface IClientDAO
    {
        public Client CreateClient(Client client);
        public Client UpdateClient(Client client);
        public void DeleteClient(Client client);
        public List<Client> GetAllClients();
        public Client GetClientById(int id);
    }
}
