using LesBooks.Application.Requests.Stock;
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
        public Task<CreateLockResponse> CreateTemporaryBlock(CreateLockRequest request);
      
    }
}
