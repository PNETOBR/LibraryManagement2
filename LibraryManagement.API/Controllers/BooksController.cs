using LibraryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core.Entities;
using LibraryManagement.Application.Models.DTOs.Outputs;
using LibraryManagement.Application.Models.DTOs.Inputs;
using LibraryManagement.Application.Models.DTOs.Result;

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
    public async Task<IActionResult> GetAll(int id = 0)
    {
        if (id > 0)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                var bookViewModel = new BookViewModel(
                book.Id, book.Title, book.Author, book.Amount, book.ISBN, book.PublishYear);
                return Ok(ResultViewModel<BookViewModel>.Ok(bookViewModel, "Livro Encontrado"));
            }
            else
                return NotFound(ResultViewModel<string>.Fail("Livro Não Encontrado"));
        }
        var books = await _context.Books
            .AsNoTracking()
            .OrderBy(b => b.Id)
            .Where(b => b.IsDelete == false)
            .Select(b => new BookViewModel(
                b.Id, b.Title, b.Author, b.Amount, b.ISBN, b.PublishYear))
            .ToListAsync();

        if (books == null || !books.Any())
            return NotFound(ResultViewModel<List<BookViewModel>>.Fail("Nenhum livro encontrado."));

        return Ok(ResultViewModel<List<BookViewModel>>.Ok(books, "livros encontrados"));
    }


    [HttpPost]//api/create
    public async Task<IActionResult> Create([FromBody] CreateBookInputModel model)
    {
        var book = new Books(
            model.Title,
            model.Author,
            1,
            model.ISBN,
            model.YearOfPublication?.Year.ToString()
        );
        try
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = ex.InnerException?.Message ?? ex.Message });
        }
        
        return CreatedAtAction(nameof(GetAll), new { id = book.Id }, book);

    }

    [HttpPut("{id}")]//api/updateAll/id
    public async Task<IActionResult> UpdateAll(int id, Books bookdb)
    {
        var bookDb = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            
        if(bookDb == null)
        {
            return NotFound();
        }
        // Atualizando os campos desejados
        bookDb.Title = bookdb.Title;
        bookDb.Author = bookdb.Author;
        bookDb.Amount = bookdb.Amount;
        bookDb.ISBN = bookdb.ISBN;
        bookDb.PublishYear = bookdb.PublishYear;

        return await UpdateDb(bookDb);
    }

    [HttpPut("{id}/amount")]//api/updateAmount/id/amount

    public async Task<IActionResult> UpdateAmount(int id, int amount)
    {
        var bookDb = await _context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();

        if(bookDb != null)
        {
            bookDb.Amount = amount;

            return await UpdateDb(bookDb);
        }

        return NotFound();

    }

    private async Task<IActionResult> UpdateDb(Books book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpDelete("{id}")]//api/delete/id
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();

        if (book == null)
        {
            return NotFound(new { message = "Livro não encontrado." });
        }
        book.IsDelete = true;
        await UpdateDb(book);

        return Ok($"O Livro nome: ({book.Title}) foi deletado");
    }


}
