using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.EmailSender
{
    public class SmtpSettings
    {
        public string Server
        {
            get
            {
                return "smtp.gmail.com";
            }
        }
        public int Port
        {
            get
            {
                return 587;
            }
        }
        public string Username
        {
            get
            {
                return "sqconsstmp@gmail.com";
            }
        }
        public string Password
        {
            get
            {
                return "cewmusvkxtqbvpzl";
            }
        }
        public string SenderName
        {
            get
            {
                return "LesBooks Contato";
            }
        }
        public string SenderEmail
        {
            get
            {
                return "sqconsstmp@gmail.com";
            }
        }
    }
}
