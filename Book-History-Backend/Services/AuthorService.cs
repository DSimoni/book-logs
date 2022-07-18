using Book_History_Backend.Data;
using Book_History_Backend.Data.Models;

namespace Book_History_Backend.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AuthorService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public IList<Author> GetAuthors()
        {
            return _applicationDbContext.Authors.ToList();
        }
    }
}
