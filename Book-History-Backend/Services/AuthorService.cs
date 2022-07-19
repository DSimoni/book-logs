using Book_History_Backend.Data;
using Book_History_Backend.Data.Models;

namespace Book_History_Backend.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;

        public AuthorService(ApplicationDbContext applicationDbContext, ILogger<BookService> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }


        public IList<Author> GetAuthors()
        {
            _logger.LogInformation($"The authors loaded",
               DateTime.UtcNow.ToLongTimeString());

            return _applicationDbContext.Authors.ToList();
        }
    }
}
