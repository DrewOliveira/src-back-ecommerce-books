namespace LesBooks.Model.Entities
{
    public class Card
    {
        public int Id { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public string securityCode { get; set; }
        public bool principal { get; set; }
        public DateTime expiration { get; set; }
        public Flag flag { get; set; }

    }
}