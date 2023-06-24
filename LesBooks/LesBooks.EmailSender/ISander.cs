namespace LesBooks.EmailSender
{
    public interface ISender
    {
        public void SendEmail(string toAddress, string subject, string clientName, string orderNumber);
    }
}