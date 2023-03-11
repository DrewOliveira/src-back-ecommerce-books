using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Client
{
    public class ClientService : IClientService
    {
        IClientDAO _clientDAO;
        public ClientService(IClientDAO clientDAO)
        {
            _clientDAO = clientDAO;
        }
        public Task<CreateClientResponse> CreateClient(CreateClientRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListClientsResponse> GetCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListClientsResponse> ListClientes()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateClientResponse> UpdateClient(UpdateClientRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
