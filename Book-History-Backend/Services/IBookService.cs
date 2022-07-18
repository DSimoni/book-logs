using Book_History_Backend.Data.Models;
using Book_History_Backend.Service_Model;

namespace Book_History_Backend.Services
{
    public interface IBookService
    {
        void AddBook(Bookdto book);
        void DeleteBook(int id);
        Bookdto GetBook(int id);
        IList<Book> GetBooks();
        IList<Book> OrderBooks();
        IList<Book> SearchBooks(string title);
        void UpdateBook(int id, Bookdto book);
    }
}