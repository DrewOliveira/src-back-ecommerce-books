using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public interface IStockRedis
    {
        public DateTime CreateTemporaryBlock(string clientId, string bookId, int quantity, int timeBlock);
        public int getTemporaryBlockbyBook(string bookId);
    }
}
