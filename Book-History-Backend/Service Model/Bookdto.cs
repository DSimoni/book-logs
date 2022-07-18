using Book_History_Backend.Data.Models;

namespace Book_History_Backend.Service_Model
{
    public class Bookdto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }

        public ICollection<Authorsdto> Authors { get; set; }
    }

    public class Authorsdto
    {
        public int AuthorId { get; set; }

        public string AuthorName { get; set; }
    }
}
