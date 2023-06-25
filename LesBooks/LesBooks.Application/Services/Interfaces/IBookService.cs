using LesBooks.Application.Requests;
using LesBooks.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<GetAllBookResponse> GetAllBooks();
        Task<GetBookResponse> GetBook(int id);

        Task<ManageBookAtivationResponse> ManageBookAtivation(int id, ManageStatusBookIdRequest manageStatusBookIdRequest);

        Task<EntryStockBookByIdResponse> entryStokBookId(int id, EntryStockRequest entryStockRequest);
    }
}
