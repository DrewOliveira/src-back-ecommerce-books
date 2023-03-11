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
        public Card CreateCard(Card card);
        public Card UpdateCard(Card card);
        public void DeleteCard(Card card);
        public List<Card> GetAllCards();
        public Card GetCardById(int id);
    }
}
