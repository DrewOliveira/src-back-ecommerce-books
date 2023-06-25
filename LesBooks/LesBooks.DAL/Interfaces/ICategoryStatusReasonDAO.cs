using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface ICategoryStatusReasonDAO
    {
        public CategoryStatusReason GetCategoryStatusReasonById(int id);

        public Task<CategoryStatusReason> GetCategoryStatusReasonByDescription(string description);

        public Task<CategoryStatusReason> CreateCategoryStatusReason(CategoryStatusReason categoryStatusReason);
    }
}

