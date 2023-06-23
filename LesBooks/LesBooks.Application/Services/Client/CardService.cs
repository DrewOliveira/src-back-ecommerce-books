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
    public class CardService : ICardService
    {
        ICardDAO _cardDAO;
        public CardService(ICardDAO cardDAO)
        {
            _cardDAO = cardDAO;
        }
        public async Task<CreateCardResponse> CreateCard(CreateCardRequest request)
        {
            CreateCardResponse response = new CreateCardResponse();
            try
            {
                Card card =  new Card();
                card.name = request.name;
                card.flag = request.flag;
                card.principal = request.principal;
                card.expiration = request.expiration;
                card.securityCode = request.securityCode;
                card.number = request.number;

                card = _cardDAO.CreateCard(request.id_client, card);
                response.id = card.Id;
            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };

            }
            return response;
        }

        public Task<ResponseBase> DeleteCard(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<GetCardResponse> GetCarde(int id)
        {

            GetCardResponse response = new GetCardResponse();
            response.card = _cardDAO.GetCardById(id);

            return response;
        }

        public async Task<ListCardsResponse> ListCardes(int id)
        {
            
            ListCardsResponse response = new ListCardsResponse();
            response.cards = _cardDAO.GetAllCards(id);
            return response;
        }

        public async Task<UpdateCardResponse> UpdateCard(UpdateCardRequest request)
        {
            UpdateCardResponse response = new UpdateCardResponse();
            try
            {
                Card card = new Card();
                card.Id = request.id;
                card.name = request.name;
                card.flag = request.flag;
                card.principal = request.principal;
                card.expiration = request.expiration;
                card.securityCode = request.securityCode;
                card.number = request.number;
                _cardDAO.UpdateCard(card);
            }
            catch (Exception ex)
            {
                response.erros = new Erro { descricao = ex.Message, detalhes = ex };

            }
            return response;
        }
    }
}
