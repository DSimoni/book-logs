using Book_History_Backend.Data.Models;
using Book_History_Backend.Service_Model;
using Book_History_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Book_History_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("{id}")]

        public IActionResult Book(int id)
        {
            return Ok(_bookService.GetBook(id));
        }


        [HttpPost]
        public IActionResult Add([FromBody] Bookdto book)
        {
            _bookService.AddBook(book);
            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id, Bookdto book)
        {
            _bookService.UpdateBook(id, book);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }

        [HttpGet("/order")]
        public IActionResult Book()
        {
            return Ok(_bookService.OrderBooks());
        }

        [HttpGet("/search")]
        public IActionResult Book([FromQuery(Name = "title")] string title)
        {
            return Ok(_bookService.SearchBooks(title));
        }

    }
}
