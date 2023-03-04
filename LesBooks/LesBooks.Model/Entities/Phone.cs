using LesBooks.Model.Enums;

namespace LesBooks.Model.Entities
{
    public class Phone
    {
        public int id { get; set; }
        public string phoneNumber { get; set; }
        public string ddd { get; set; }
        public TypePhone typePhone { get; set; }
    }
}