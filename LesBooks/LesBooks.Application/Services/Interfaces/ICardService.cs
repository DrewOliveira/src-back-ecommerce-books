using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface ICardService
    {
        Task<CreateCardResponse> CreateCard(CreateCardRequest request);
        Task<UpdateCardResponse> UpdateCard(UpdateCardRequest request);
        Task<ResponseBase> DeleteCard(int id);
        Task<ListCardsResponse> ListCardes(int id);
        Task<GetCardResponse> GetCarde(int id);
    }
}
