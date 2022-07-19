using Book_History_Backend.Data;
using Book_History_Backend.Data.Models;
using Book_History_Backend.Service_Model;

namespace Book_History_Backend.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;


        public BookService(ApplicationDbContext applicationDbContext, ILogger<BookService> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public IList<Book> GetBooks()
        {
            _logger.LogInformation($"The books loaded",
                  DateTime.UtcNow.ToLongTimeString());

            return _applicationDbContext.Books.ToList();
        }

        public IList<Book> OrderBooks()
        {
            _logger.LogInformation($"The books were sorted",
              DateTime.UtcNow.ToLongTimeString());

            return _applicationDbContext.Books.OrderBy(x => x.Title).ToList();
        }

        public IList<Book> SearchBooks(string title)
        {
            _logger.LogInformation($"The book with id: {title} was searched",
             DateTime.UtcNow.ToLongTimeString());

            return _applicationDbContext.Books.Where(x => x.Title.Contains(title)).ToList();
        }


        public Bookdto GetBook(int id)
        {

            Book? foundBook = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);

            if (foundBook != null)
            {
                IQueryable<Authorsdto> authorsofBook = (from b in _applicationDbContext.Books
                                                        join ab in _applicationDbContext.AuthorBooks on b.Id equals ab.Id
                                                        join a in _applicationDbContext.Authors on ab.AuthorId equals a.AuthorId
                                                        where b.Id == id
                                                        select new Authorsdto
                                                        {
                                                            AuthorId = a.AuthorId,
                                                            AuthorName = a.AuthorName
                                                        });


                Bookdto book = new Bookdto
                {
                    Id = id,
                    Title = foundBook.Title,
                    Description = foundBook.Description,
                    PublishDate = foundBook.PublishDate,
                    Authors = authorsofBook.ToList()
                };

                _logger.LogInformation($"The book with id: {id} has {book.Title} ,description {book.Description} and published date {book.PublishDate}",
                      DateTime.UtcNow.ToLongTimeString());

                return book;
            }
            else
            {
                _logger.LogWarning($"Can't find book with id {id}");
                return new Bookdto();
            }
        }


        public void AddBook(Bookdto bookdto)
        {
            using (var transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    Book book = new Book
                    {
                        Title = bookdto.Title,
                        Description = bookdto.Description,
                        PublishDate = DateTime.Now,
                    };

                    _applicationDbContext.Books.Add(book);
                    _applicationDbContext.SaveChanges();

                    Int32 lastifOfBook = _applicationDbContext.Books.OrderByDescending(x => x.Id).First().Id; //Get last element inserted


                    List<Author> AuthorBooks = _applicationDbContext.Authors.Where(i =>
                                       bookdto.Authors.Select(AuthorId => AuthorId.AuthorId)
                                       .Contains(i.AuthorId)).ToList();


                    foreach (Author Author in AuthorBooks)
                    {
                        AuthorBook authorBook = new AuthorBook();

                        authorBook.Id = lastifOfBook;
                        authorBook.AuthorId = Author.AuthorId;

                        _applicationDbContext.AuthorBooks.Add(authorBook);
                    }


                    _applicationDbContext.SaveChanges();
                    transaction.Commit();

                    _logger.LogInformation($"The book {book.Title} added with description {book.Description} and published date {book.PublishDate}",
                    DateTime.UtcNow.ToLongTimeString());

                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error adding ${ex.Message} The book {bookdto.Title} added with description {bookdto.Description} and published date {bookdto.PublishDate}",
                      DateTime.UtcNow.ToLongTimeString());

                    transaction.Rollback();
                }
            }
        }

        public void UpdateBook(int id, Bookdto book)
        {
            Book? foundBook = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);

            if (foundBook != null)
            {
                using (var transaction = _applicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        IQueryable<AuthorBook> listAuthors = _applicationDbContext.AuthorBooks.Where(x => x.Id == id);

                        _applicationDbContext.AuthorBooks.RemoveRange(listAuthors);  //Clean all previous selected
                                                                                     //_applicationDbContext.SaveChanges();

                        foreach (var author in book.Authors)
                        {
                            AuthorBook authorBook = new AuthorBook();

                            authorBook.Id = id;
                            authorBook.AuthorId = author.AuthorId;

                            _applicationDbContext.AuthorBooks.Add(authorBook); //Add selected values
                        }



                        _logger.LogInformation($"The book with title {foundBook.Title} updated to {book.Title}",
                        DateTime.UtcNow.ToLongTimeString());

                        foundBook.Title = book.Title;
                        foundBook.Description = book.Description;

                        _applicationDbContext.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error adding ${ex.Message}  The book {book.Title} added with description {book.Description} and published date {book.PublishDate}",
                          DateTime.UtcNow.ToLongTimeString());

                        transaction.Rollback();
                    }
                }
            }
            else
            {
                _logger.LogWarning($"Can't find book with id {id}");
            }
        }

        public void DeleteBook(int id)
        {
            Book? foundBook = _applicationDbContext.Books.FirstOrDefault(x => x.Id == id);

            if (foundBook != null)
            {
                using (var transaction = _applicationDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        IQueryable<AuthorBook> foundBookAuthor = _applicationDbContext.AuthorBooks.Where(x => x.Id == id);

                        if (foundBookAuthor.Count() > 0)
                        {
                            _applicationDbContext.AuthorBooks.RemoveRange(foundBookAuthor);
                        }

                        _applicationDbContext.Books.Remove(foundBook);

                        _applicationDbContext.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Error delete The book {foundBook?.Title}:  {ex.Message}  ",
                          DateTime.UtcNow.ToLongTimeString());

                        transaction.Rollback();
                    }
                }
            }
            else
            {
                _logger.LogWarning($"Can't find book with id {id}");
            }
        }

    }





}

