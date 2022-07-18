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
        public async Task<IActionResult> Index()
        {
            return Ok(_bookService.GetBooks());
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> Book(int id)
        {
            return Ok(_bookService.GetBook(id));
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Bookdto book)
        {
            _bookService.AddBook(book);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, Bookdto book)
        {
            _bookService.UpdateBook(id, book);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _bookService.DeleteBook(id);
            return Ok();
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Book([FromQuery(Name = "title")] string title)
        {
            return Ok(_bookService.SearchBooks(title));
        }

    }
}
