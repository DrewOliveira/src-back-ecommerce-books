using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface IClientService
    {
        Task<CreateClientResponse> CreateClient(CreateClientRequest request);
        Task<UpdateClientResponse> UpdateClient(UpdateClientRequest request);
        Task<ResponseBase> DeleteClient(int id);
        Task<ListClientsResponse> ListClientes();
        Task<GetClientResponse> GetCliente(int id);
        Task<ResponseBase> ChangePassword(ChangePasswordClientRequest request);
    }
}
