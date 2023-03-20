using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL.Interfaces
{
    public interface IFlagDAO
    {
        public Flag GetFlagById(int id);
        public List<Flag> GetAllFlags();

    }
}
