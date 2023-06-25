using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using LesBooks.Application.Services.Interfaces;
using LesBooks.DAL.DAOs;
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
        ICategoryStatusReasonDAO _categoryStatusReasonDAO;
        IActivationStatusReasonDAO _activationStatusReasonDAO;
        IStockDAO _stockDAO;
        IStockEntryHistoryDAO _stockEntryHistoryDAO;

        public BookService(IBookDAO ibookDAO, ICategoryStatusReasonDAO icategoryStatusReasonDAO, IActivationStatusReasonDAO activationStatusReasonDAO, IStockDAO stockDAO, IStockEntryHistoryDAO stockEntryHistoryDAO)
        {
            _bookDAO = ibookDAO;
            _categoryStatusReasonDAO = icategoryStatusReasonDAO;
            _activationStatusReasonDAO = activationStatusReasonDAO;
            _stockDAO = stockDAO;
            _stockEntryHistoryDAO = stockEntryHistoryDAO;
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

        public async Task<ManageBookAtivationResponse> ManageBookAtivation(int id, ManageStatusBookIdRequest manageStatusBookIdRequest)
        {
            ManageBookAtivationResponse manageAtivationResponse = new ManageBookAtivationResponse();
            
            try
            {
                Book book = new Book();
                ActivationStatusReason activationStatusReason = new ActivationStatusReason();
                CategoryStatusReason categoryStatusReason = new CategoryStatusReason();

                book = _bookDAO.GetBookById(id);

                book.active = book.active ? false : true;
                

                categoryStatusReason = await _categoryStatusReasonDAO.GetCategoryStatusReasonByDescription(manageStatusBookIdRequest.categoryStatusReasonDescription);

                if (categoryStatusReason.id == 0)
                {
                    categoryStatusReason.description = manageStatusBookIdRequest.categoryStatusReasonDescription;
                    categoryStatusReason = await _categoryStatusReasonDAO.CreateCategoryStatusReason(categoryStatusReason);
                }
                
                activationStatusReason.justification = manageStatusBookIdRequest.manageStatusReasonDescription;
                activationStatusReason.status = book.active;
                activationStatusReason.categoryStatusReason = categoryStatusReason;
                
                book.activationStatusReason = await _activationStatusReasonDAO.CreateActivationStatusReason(activationStatusReason);

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

        public async Task<EntryStockBookByIdResponse> entryStokBookId(int id, EntryStockRequest entryStockRequest)
        {
            EntryStockBookByIdResponse entryStockBookByIdResponse = new EntryStockBookByIdResponse();
            try
            {
                Double higherCostValue = 0;

                if (entryStockRequest.quantity == 0)
                {
                    throw new Exception("Quantity equal 0");
                }

                if (entryStockRequest.costValue <= 0)
                {
                    throw new Exception("costValue not valide");
                }

                Book book = new Book();
                book = _bookDAO.GetBookById(id);

                if (book.id != 0)
                {
                    Stock stock = new Stock();
                    StockEntryHistory stockEntryHistory = new StockEntryHistory();

                    higherCostValue = book.stock.costValue > entryStockRequest.costValue ? book.stock.costValue : entryStockRequest.costValue;

                    if (book.stock.id != 0)
                    {
                        stock.quantity = book.stock.quantity + entryStockRequest.quantity;
                        stock.costValue = higherCostValue;
                        stock.id = book.stock.id;

                        _stockDAO.EntryStockBookId(book.id, stock.quantity, stock.costValue);
                    }
                    else
                    {
                        stock = await _stockDAO.CreateStockByBookId(book.id, stock);
                    }

                    stockEntryHistory.entryDate = DateTime.Now;
                    stockEntryHistory.quantity = entryStockRequest.quantity;
                    stockEntryHistory.costValue = entryStockRequest.costValue;
                    stockEntryHistory.stockId = stock.id;

                    await _stockEntryHistoryDAO.CreateStockEntryHistory(stockEntryHistory);
                }
                
                entryStockBookByIdResponse.book = _bookDAO.GetBookById(id);
            }
            catch (Exception err)
            {
                entryStockBookByIdResponse.erros = new Erro
                {
                    descricao = err.Message,
                    detalhes = err
                };
            }

            return entryStockBookByIdResponse;
        }

        //private Double calculateValueSale(Double costValue, int minPrefixCostValue, int maxPrefixCostValue)
        //{
        //    Double valueWithMinPrefixCostValue = (costValue / 100) * minPrefixCostValue;
        //    Double valueWithMaxPrefixCostValue = (costValue / 100) * maxPrefixCostValue;
        //}
    }
}
