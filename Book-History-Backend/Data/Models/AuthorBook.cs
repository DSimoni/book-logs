namespace Book_History_Backend.Data.Models
{
    public class AuthorBook
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
