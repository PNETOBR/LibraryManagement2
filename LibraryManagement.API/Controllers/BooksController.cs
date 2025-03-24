using LibraryManagement.API.Infraestructure.Persistence;
using LibraryManagement.API.Model;
using LibraryManagement.API.ViewModels.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{

    private readonly LibraryManagementDbContext _context;

    public BooksController(LibraryManagementDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _context.Books
            .OrderBy(b => b.Title)
            .Select(b => new BookViewModel(
                b.Id, b.Title, b.Author, b.Amount, b.ISBN, b.PublishYear))
            .ToListAsync();
            

        return Ok(books);
    }

    // GET: api/books
    //[HttpGet]
    //public IActionResult Get()
    //{
    //    var BookViewModel = new BookViewModel(1, "O Senhor dos Anéis", "J.R.R. Tolkien", 10, 9788578, "Martins Fontes");
    //    return Ok(BookViewModel);
    //}

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(int id, CreateBookInputModel model)
    {

        return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }
}
