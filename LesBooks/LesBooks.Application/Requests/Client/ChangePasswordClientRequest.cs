using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class ChangePasswordClientRequest
    {
        public string newPassword { get; set; }
        public string oldPassword { get; set; }
        public int id { get; set; }
    }
}
