using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LesBooks.Application.Services
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
            Model.Entities.Client client = new Model.Entities.Client();
            client.cpf = request.cpf;
            client.phone = request.phone;
            client.name = request.name;
            client.user = request.user;
            client.gender = request.gender;
            client.adresses = new List<Adress>();
            foreach(var adress in request.adresses)
            {
                client.adresses.Add(new Adress()
                {
                    typeAdress = adress.typeAdress,
                    number = adress.number,
                    city = adress.city, 
                    country= adress.country,   
                    identification = adress.identification,
                    neighborhood = adress.neighborhood,
                    obs = adress.obs,
                    state = adress.state,
                    street = adress.street,
                    typeResidence = adress.typeResidence,
                    typeStreet = adress.typeStreet,
                    zipCode = adress.zipCode 
                });
            }
            client.card = new List<Card>();
            foreach(var card in request.cards)
            {
                client.card.Add(new Card()
                {
                    flag = card.flag,
                    name = card.name,
                    number = card.number,
                    pricipal = card.pricipal,
                    securityCode = card.securityCode
                });
            }


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
            Model.Entities.Client client = new Model.Entities.Client();
            client.id = request.id;
            client.cpf = request.cpf;
            client.phone = request.phone;
            client.name = request.name;
            client.user = request.user;
            client.gender = request.gender;

            throw new NotImplementedException();
        }
    }
}
