using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.DAL
{
    public interface IUserDAO
    {
        public User CreateUser(User user);
        public User UpdateUser(User user);
        public void DeleteUser(User user);
        public List<User> GetAllUsers();
        public User GetUserById(int id);
    }
}
