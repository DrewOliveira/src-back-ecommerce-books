using AngleSharp.Io;
using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.Application.Strategies;
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

        public async Task<ResponseBase> ChangePassword(ChangePasswordClientRequest request)
        {
            string oldPassword = request.oldPassword;
            string nesPassword = request.newPassword;
            int id = request.id;
            Model.Entities.Client client = _clientDAO.GetClientById(id);
            ResponseBase response = new ResponseBase();
            try
            {
                
            }
            catch(Exception ex)
            {

            }
            return response;
        }

        public async Task<CreateClientResponse> CreateClient(CreateClientRequest request)
        {
            CreateClientResponse response = new CreateClientResponse();
            try
            {
                Model.Entities.Client client = new Model.Entities.Client();
                client.cpf = request.cpf;
                client.phone = request.phone;
                client.name = request.name;
                client.user = request.user;
                client.user.password = Criptografa.Encrypt(client.user.password);
                client.gender = request.gender;
                client.birth = request.birth;
                client.adresses = new List<Adress>();
                foreach (var adress in request.adresses)
                {
                    client.adresses.Add(new Adress()
                    {
                        typeAdress = adress.typeAdress,
                        number = adress.number,
                        city = adress.city,
                        country = adress.country,
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
                foreach (var card in request.cards)
                {
                    client.card.Add(new Card()
                    {
                        flag = card.flag,
                        name = card.name,
                        number = card.number,
                        pricipal = card.pricipal,
                        securityCode = card.securityCode,
                        expiration = card.expiration
                        
                    });
                }

                _clientDAO.CreateClient(client);
            }
            catch (Exception ex)
            {
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });

            }
            return response;
        }

        public async Task<ResponseBase> DeleteClient(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetClientResponse> GetCliente(int id)
        {
            GetClientResponse response = new GetClientResponse();
            response.client = _clientDAO.GetClientById(id);
            return response;
        }

        public async Task<ListClientsResponse> ListClientes()
        {

            ListClientsResponse response = new ListClientsResponse();
            response.clients = _clientDAO.GetAllClients();
            return response;
        }

        public async Task<UpdateClientResponse> UpdateClient(UpdateClientRequest request)
        {
            UpdateClientResponse response = new UpdateClientResponse();
            try
            {
                Model.Entities.Client client = new Model.Entities.Client();
                client.id = request.id;
                client.cpf = request.cpf;
                client.phone = request.phone;
                client.name = request.name;
                client.user = request.user;
                client.gender = request.gender;
                _clientDAO.UpdateClient(client);

            }
            catch (Exception ex)
            {
                response.erros.Add(new Erro { descricao = ex.Message, detalhes = ex });

            }
            return response;

        }
    }
}
