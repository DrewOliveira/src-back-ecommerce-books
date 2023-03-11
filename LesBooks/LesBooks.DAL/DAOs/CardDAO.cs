using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public class CardDAO : Connection, ICardDAO
    {
        public Card CreateCard(Card card)
        {
            throw new NotImplementedException();
        }

        public void DeleteCard(Card card)
        {
            throw new NotImplementedException();
        }

        public List<Card> GetAllCards()
        {
            throw new NotImplementedException();
        }

        public Card GetCardById(int id)
        {
            throw new NotImplementedException();
        }

        public Card UpdateCard(Card card)
        {
            throw new NotImplementedException();
        }
    }
}
