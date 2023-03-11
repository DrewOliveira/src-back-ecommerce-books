
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class ClientDAO : Connection, IClientDAO
    {
        public Client CreateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public void DeleteClient(Client client)
        {
            throw new NotImplementedException();
        }

        public List<Client> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public Client GetClientById(int id)
        {
            throw new NotImplementedException();
        }

        public Client UpdateClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
