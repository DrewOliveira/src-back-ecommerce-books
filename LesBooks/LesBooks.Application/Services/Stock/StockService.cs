using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL;
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
        IStockRedis _stockRedis;
        public StockService(IStockDAO stockDAO,IStockRedis stockRedis)
        {
            _stockDAO = stockDAO;
            _stockRedis = stockRedis;
        }
        public async Task<ResponseBase> CreateTemporaryBlock(string clientId, string bookId, int quantity)
        {
            ResponseBase responseBase  =  new ResponseBase();
            try
            {
                _stockRedis.CreateTemporaryBlock(clientId, bookId, quantity, 2);    

            }catch(Exception ex)
            {
                responseBase.erros = new List<Erro>()
                {
                    new Erro()
                    {
                        descricao = ex.Message,
                        detalhes = ex
                    }
                };
            }
            return responseBase;
        }
        public async Task<ValidateStockByBookIdResponse> ValidateStockByBookId(int bookId, int quantity)
        {
            ValidateStockByBookIdResponse validateStockByBookIdResponse = new ValidateStockByBookIdResponse();
            validateStockByBookIdResponse.validate = false;

            try
            {
                Stock stock = _stockDAO.GetStockByBookId(bookId);

                int blockedStock = _stockRedis.getTemporaryBlockbyBook(bookId.ToString());
                int freeStock = stock.quantity - blockedStock;

                if (quantity <= freeStock)
                {
                    validateStockByBookIdResponse.validate = true;
                }

                validateStockByBookIdResponse.quantity = freeStock;

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
