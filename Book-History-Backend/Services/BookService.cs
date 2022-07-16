using Book_History_Backend.Data;
using Book_History_Backend.Data.Models;

namespace Book_History_Backend.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IList<Book> GetBooks()
        {
            return _applicationDbContext.Books.ToList();
        }

        public IList<Book> OrderBooks()
        {
            return _applicationDbContext.Books.ToList();
        }

        public IList<Book> SearchBooks()
        {
            return _applicationDbContext.Books.ToList();
        }



        public Book GetBook()
        {
            return new Book();
        }


        public void AddBook(Book book)
        {
        }

        public void UpdateBook(int id, Book book)
        {
        }

        public void DeleteBook(int id)
        {
        }
    }
}

