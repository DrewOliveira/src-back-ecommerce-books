using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL.Interfaces;
using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services
{
    public class BookService : IBookService
    {
        IBookDAO _bookDAO;
        public BookService(IBookDAO ibookDAO)
        {
            _bookDAO = ibookDAO;
        }
        public async Task<GetBookResponse> GetBook(int id)
        {
            GetBookResponse getBookResponse = new GetBookResponse();

            try
            {
                getBookResponse.book = _bookDAO.GetBookById(id);
            }
            catch (Exception err)
            {
                getBookResponse.erros = new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                };
            }

            return getBookResponse;

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
                getAllBookResponse.erros = new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                };
            }

            return getAllBookResponse;

        }

        public async Task<ManageBookAtivationResponse> ManageBookAtivation(int id)
        {
            ManageBookAtivationResponse manageAtivationResponse = new ManageBookAtivationResponse();
            
            try
            {
                Book book = new Book();
                book = _bookDAO.GetBookById(id);

                book.active = book.active ? false : true;
                
                manageAtivationResponse.book = _bookDAO.ChangeBook(book);
            }
            catch(Exception err)
            {
                manageAtivationResponse.erros = new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                };
            }

            return manageAtivationResponse;
        }
    }
}
