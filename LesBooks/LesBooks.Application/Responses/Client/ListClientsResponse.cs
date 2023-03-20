using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LesBooks.Model.Entities;

namespace LesBooks.Application.Responses
{
    public class ListClientsResponse : ResponseBase
    {
        public List<Client> clients;
    }
}
