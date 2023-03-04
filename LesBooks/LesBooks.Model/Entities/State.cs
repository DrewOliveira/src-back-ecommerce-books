namespace LesBooks.Model.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string description { get; set; }
        public Country country { get; set; }
    }
}