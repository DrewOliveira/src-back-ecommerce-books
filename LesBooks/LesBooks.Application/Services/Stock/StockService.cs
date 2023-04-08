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
    public class StockService: IStockService
    {
        IStockDAO _stockDAO;
        public StockService(IStockDAO stockDAO)
        {
            _stockDAO = stockDAO;
        }

        public async Task<ValidateStockByBookIdResponse> ValidateStockByBookId(int bookId, int quantity)
        {
            ValidateStockByBookIdResponse validateStockByBookIdResponse = new ValidateStockByBookIdResponse();
            validateStockByBookIdResponse.validate = false;

            try
            {
                Stock stock = _stockDAO.GetStockByBookId(bookId);

                if (quantity <= stock.quantity)
                {
                    validateStockByBookIdResponse.validate = true;
                }

                validateStockByBookIdResponse.quantity = stock.quantity;

            }
            catch (Exception err)
            {
                validateStockByBookIdResponse.erros.Add(new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                });
            }

            return validateStockByBookIdResponse;
        }
    }
}
