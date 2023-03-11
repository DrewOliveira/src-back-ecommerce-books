using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services
{
    public class CardService : ICardService
    {
        ICardDAO _cardDAO;
        public CardService(ICardDAO cardDAO)
        {
            _cardDAO = cardDAO;
        }
        public Task<CreateCardResponse> CreateCard(CreateCardRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> DeleteCard(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListCardsResponse> GetCarde(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListCardsResponse> ListCardes()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateCardResponse> UpdateCard(UpdateCardRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
