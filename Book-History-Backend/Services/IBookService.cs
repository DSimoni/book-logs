using Book_History_Backend.Data.Models;

namespace Book_History_Backend.Services
{
    public interface IBookService
    {
        void AddBook(Book book);
        void DeleteBook(int id);
        Book GetBook(int id);
        IList<Book> GetBooks();
        IList<Book> OrderBooks();
        IList<Book> SearchBooks(string title);
        void UpdateBook(int id, Book book);
    }
}