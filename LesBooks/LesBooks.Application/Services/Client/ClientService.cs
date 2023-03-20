using AngleSharp.Io;
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

        public async Task<ResponseBase> ChangePassword(ChangePasswordClientRequest request)
        {

            throw new NotImplementedException();
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
                client.gender = request.gender;
                client.birth = request.birth;
               
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

        public async Task<ListClientsResponse> GetCliente(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ListClientsResponse> ListClientes()
        {

            ListClientsResponse response = new ListClientsResponse();
            response.clients = new List<Model.Entities.Client>();
            Adress adress = new Adress()
            {
                id = 1,
                street = "Rua Monte Alto res",
                number = "343",
                neighborhood = "Tipoia",
                city = "Itaquaquecetuba",
                state = "SP",
                country = "Brasil",
                obs = "",
                typeAdress = Model.Enums.TypeAdress.RESIDENCIAL,
                typeResidence = Model.Enums.TypeResidence.casa,
                typeStreet = Model.Enums.TypeStreet.rua,
                zipCode = "08577130"
            };
            Adress adress1 = new Adress()
            {
                id = 1,
                street = "Rua Monte Alto ent",
                number = "343",
                neighborhood = "Tipoia",
                city = "Itaquaquecetuba",
                state = "SP",
                country = "Brasil",
                obs = "",
                identification = "entrega 1",
                typeAdress = Model.Enums.TypeAdress.ENTREGA,
                typeResidence = Model.Enums.TypeResidence.casa,
                typeStreet = Model.Enums.TypeStreet.rua,
                zipCode = "08577130"
            };
            Adress adress2 = new Adress()
            {
                id = 1,
                street = "Rua Monte Alto cob",
                number = "343",
                neighborhood = "Tipoia",
                city = "Itaquaquecetuba",
                state = "SP",
                country = "Brasil",
                obs = "",
                typeAdress = Model.Enums.TypeAdress.COBRANCA,
                typeResidence = Model.Enums.TypeResidence.casa,
                typeStreet = Model.Enums.TypeStreet.rua,
                zipCode = "08577130"
            };
            Card card = new Card()
            {
                Id = 1,
                name = "Andrew Santos",
                number = "0987123412341234",
                pricipal = true,
                securityCode = "123",
                flag = new Flag()
                {
                    Id = 1,
                    description = "Mastercard"
                }
            };
            Card card1 = new Card()
            {
                Id = 2,
                name = "Andrew Santos 2",
                number = "4321432143214321",
                pricipal = false,
                securityCode = "321",
                flag = new Flag()
                {
                    Id = 1,
                    description = "Visa"
                }
            };
            Model.Entities.Client client = new Model.Entities.Client()
            {
                name = "Andrew de oliveira",
                cpf = "48241066805",
                gender = "M",
                user = new User()
                {
                    Id = 1,
                    email = "andrew@teste.com",
                    typeUser = Model.Enums.TypeUser.User
                },
                id = 1,
                phone = new Phone() { id = 1, ddd = "11", phoneNumber = "973900330", typePhone = Model.Enums.TypePhone.celular },
                ranking = new Ranking() { id = 1 , lesBookPoints = 1002}
                
            };
            client.adresses = new List<Adress>();
            client.adresses.Add(adress);
            client.adresses.Add(adress1);
            client.adresses.Add(adress2);
            client.card = new List<Card>();
            client.card.Add(card);
            client.card.Add(card1);
            response.clients.Add(client);
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
