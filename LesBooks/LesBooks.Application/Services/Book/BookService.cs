using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Book
{
    public class BookService : IBookService
    {
        IBookDAO _bookDAO;
        public BookService(IBookDAO ibookDAO)
        {
            _bookDAO = ibookDAO;
        }

        public async Task<GetAllBookResponse> GetAllBooks()
        {
            GetAllBookResponse getAllBookResponse = new GetAllBookResponse();

            try
            {
                getAllBookResponse.books = _bookDAO.GetAllBooks();
            }
            catch (Exception err)
            {
                getAllBookResponse.erros.Add(new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                });
            }

            return getAllBookResponse;

        }
    }
}
