using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IBookDAO
    {
        public List<Book> GetAllBooks();

        public Book GetBookById(int id);

        public Book ChangeBook(Book book);
    }
}
