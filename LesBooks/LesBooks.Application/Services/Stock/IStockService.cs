using LesBooks.Application.Responses;
using LesBooks.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Stock
{
    public interface IStockService
    {
        //IBookDAO _bookDAO;
        //public BookService(IBookDAO ibookDAO)
        //{
        //    _bookDAO = ibookDAO;
        //}

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
