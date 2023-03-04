namespace LesBooks.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public TypeUser typeUser { get; set; }
    }
}