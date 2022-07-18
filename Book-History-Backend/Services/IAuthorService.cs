using Book_History_Backend.Data.Models;

namespace Book_History_Backend.Services
{
    public interface IAuthorService
    {
        IList<Author> GetAuthors();
    }
}