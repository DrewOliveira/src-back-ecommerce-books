using LesBooks.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Services.Interfaces
{
    public interface IStockService
    {
        public Task<ValidateStockByBookIdResponse> ValidateStockByBookId(int bookId, int quantity);
        public Task<ResponseBase> CreateTemporaryBlock(string clientId, string bookId, int quantity);
      
    }
}
