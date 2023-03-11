using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface IAdressService
    {
        Task<CreateAdressResponse> CreateAdress(CreateAdressRequest request);
        Task<UpdateAdressResponse> UpdateAdress(UpdateAdressRequest request);
        Task<ResponseBase> DeleteAdress(int id);
        Task<ListAdressResponse> ListAdresses();
        Task<ListAdressResponse> GetAdress(int id);
    }
}
