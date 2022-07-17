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

        public IList<Book> SearchBooks(string title)
        {
            return _applicationDbContext.Books.Where(x => x.Title.Contains(title)).ToList();
        }


        public Book? GetBook(int id)
        {
            return _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);
        }


        public void AddBook(Book book)
        {
            _applicationDbContext.Books.Add(book);
            _applicationDbContext.SaveChanges();
        }

        public void UpdateBook(int id, Book book)
        {
            Book? foundBook = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);

            if (foundBook != null)
            {
                foundBook.Title = book.Title;
                _applicationDbContext.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            Book? foundBook = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);

            if (foundBook != null)
            {
                _applicationDbContext.Books.Remove(foundBook);
                _applicationDbContext.SaveChanges();
            }

        }
    }
}

