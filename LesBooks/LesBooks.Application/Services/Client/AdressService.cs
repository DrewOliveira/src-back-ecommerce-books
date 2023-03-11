using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Client
{
    public class AdressService : IAdressService
    {
        public Task<CreateAdressResponse> CreateAdress(CreateAdressRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> DeleteAdress(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListAdressResponse> GetAdress(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListAdressResponse> ListAdresses()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAdressResponse> UpdateAdress(UpdateAdressRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
