using Correios.NET;
using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Client
{
    public class AdressService : IAdressService
    {
        IAdressDAO _adressDAO;
        public AdressService(IAdressDAO adressDAO)
        {
            _adressDAO = adressDAO;
        }
        public async Task<CreateAdressResponse> CreateAdress(CreateAdressRequest request)
        {
            CreateAdressResponse response = new CreateAdressResponse();
            try
            {
                Adress adress = new Adress()
                {
                   
                    street = request.street,
                    number = request.number,
                    neighborhood = request.neighborhood,
                    city = request.city,
                    state = request.state,
                    country = request.country,
                    obs = request.obs,
                    identification = request.identification,
                    typeAdress = request.typeAdress,
                    typeResidence = request.typeResidence,
                    typeStreet = request.typeStreet,
                    zipCode = request.zipCode
                };
                adress = _adressDAO.CreateAdress(request.id_client,adress);
            }
            catch(Exception ex)
            {
                response.erros = new List<Erro>()
                {
                    new Erro()
                    {
                        descricao = ex.Message,
                        detalhes = ex
                    }
                };
            }
            return response;
        }

        public Task<ResponseBase> DeleteAdress(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAdressResponse> GetAdress(int id)
        {

            GetAdressResponse response = new GetAdressResponse();
            response.adress = _adressDAO.GetAdressById(id);
            return response; 

        }

        public async Task<GetAdressResponse> GetAdressByCep(string cep)
        {
            CorreiosService correiosService = new CorreiosService();
            var adresses = correiosService.GetAddresses(cep);
            GetAdressResponse response = new GetAdressResponse();
            if (adresses.Count() == 0)
                return response;
            foreach(var item in adresses)
            {
                Adress adress = new Adress();
                adress.street = item.Street;
                adress.city = item.City;
                adress.state = item.State;
                adress.neighborhood = item.District;
                response.adress = adress;
            }
            
            return response;
        }


        public async Task<ListAdressResponse> ListAdresses(int id)
        {
            
            ListAdressResponse response = new ListAdressResponse();
            response.adress = _adressDAO.GetAllAdresss(id);
            return response;
        }

        public async Task<UpdateAdressResponse> UpdateAdress(UpdateAdressRequest request)
        {
            UpdateAdressResponse response = new UpdateAdressResponse();
            try
            {
                Adress adress = new Adress()
                {
                    id = request.id,
                    street = request.street,
                    number = request.number,
                    neighborhood = request.neighborhood,
                    city = request.city,
                    state = request.state,
                    country = request.country,
                    obs = request.obs,
                    identification = request.identification,
                    typeAdress = request.typeAdress,
                    typeResidence = request.typeResidence,
                    typeStreet = request.typeStreet,
                    zipCode = request.zipCode
                };
                adress = _adressDAO.UpdateAdress(adress);
            }
            catch (Exception ex)
            {
                response.erros = new List<Erro>()
                {
                    new Erro()
                    {
                        descricao = ex.Message,
                        detalhes = ex
                    }
                };
            }
            return response;
        }
    }
}
