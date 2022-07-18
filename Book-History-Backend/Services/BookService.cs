using Book_History_Backend.Data;
using Book_History_Backend.Data.Models;
using Book_History_Backend.Service_Model;

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
            return _applicationDbContext.Books.OrderBy(x => x.Id).ToList();
        }

        public IList<Book> SearchBooks(string title)
        {
            return _applicationDbContext.Books.Where(x => x.Title.Contains(title)).ToList();
        }


        public Bookdto? GetBook(int id)
        {

            var book = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);


            var rowList = (from b in _applicationDbContext.Books
                           join ab in _applicationDbContext.AuthorBooks on b.Id equals ab.Id
                           join a in _applicationDbContext.Authors on ab.AuthorId equals a.AuthorId
                           where b.Id == id
                           select new Authorsdto
                           {
                               AuthorId = a.AuthorId,
                               AuthorName = a.AuthorName
                           });




            Bookdto? bookAuthor = new Bookdto
            {
                Id = id,
                Title = book.Title,
                Description = book.Description,
                PublishDate = book.PublishDate,
                Authors = rowList.ToList()
            };

            return bookAuthor;
        }


        public void AddBook(Bookdto book)
        {
            try
            {
                Book bookAuthor = new Book
                {
                    Title = book.Title,
                    Description = book.Description,
                    PublishDate = book.PublishDate,
                };

                _applicationDbContext.Books.Add(bookAuthor);
                _applicationDbContext.SaveChanges();



                var AuthorBooks = _applicationDbContext.Authors.Where(i =>
                                   book.Authors.Select(AuthorId => AuthorId.AuthorId).ToList()
                                   .Contains(i.AuthorId)).ToList();



                var lastifOfBook = _applicationDbContext.Books.OrderBy(x => x.Id).Last().Id;

                foreach (var Author in AuthorBooks)
                {
                    AuthorBook authorBook = new AuthorBook();

                    authorBook.Id = lastifOfBook;
                    authorBook.AuthorId = Author.AuthorId;

                    _applicationDbContext.AuthorBooks.Add(authorBook);

                }
                _applicationDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void UpdateBook(int id, Bookdto book)
        {
            Book? foundBook = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);

            if (foundBook != null)
            {
                IQueryable<AuthorBook> listAuthors = _applicationDbContext.AuthorBooks.Where(x => x.Id == id);

                _applicationDbContext.AuthorBooks.RemoveRange(listAuthors);  //Clean all previous selected
                _applicationDbContext.SaveChanges();

                foreach (var author in book.Authors)
                {
                    AuthorBook authorBook = new AuthorBook();

                    authorBook.Id = id;
                    authorBook.AuthorId = author.AuthorId;

                    _applicationDbContext.AuthorBooks.Add(authorBook); //Add selected values
                }


                foundBook.Title = book.Title;
                foundBook.Description = book.Description;

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

            IQueryable<AuthorBook> foundBookAuthor = _applicationDbContext.AuthorBooks.Where(x => x.Id == id);

            if (foundBookAuthor.Count() > 0)
            {
                _applicationDbContext.AuthorBooks.RemoveRange(foundBookAuthor);
                _applicationDbContext.SaveChanges();
            }

        }
    }
}

