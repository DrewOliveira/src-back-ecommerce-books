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
    }
}
