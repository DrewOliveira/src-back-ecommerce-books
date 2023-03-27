using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public interface IAdressDAO
    {
        public Adress CreateAdress(int id,Adress adress);
        public Adress UpdateAdress(Adress adress);
        public void DeleteAdress(int id);
        public List<Adress> GetAllAdresss(int id);
        public Adress GetAdressById(int id);
    }
}
