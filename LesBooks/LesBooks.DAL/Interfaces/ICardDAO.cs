using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public interface ICardDAO
    {
        public Card CreateCard(int id,Card card);
        public Card UpdateCard(Card card);
        public void DeleteCard(Card card);
        public List<Card> GetAllCards(int id);
        public Card GetCardById(int id);
    }
}
