﻿using LibraryManagement.API.Entities;
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
    public async Task<IActionResult> GetAll(int id = 0)
    {
        if (id > 0)
        {
            var book = _context.Books.FindAsync(id).Result;

            if (book != null)
            {
                var bookViewModel = new BookViewModel(
                book.Id, book.Title, book.Author, book.Amount, book.ISBN, book.PublishYear);
                return Ok(bookViewModel);
            }
            else
                return NotFound("Livro Não Encontrado");
        }
        var books = await _context.Books
            .AsNoTracking()
            .OrderBy(b => b.Id)
            .Where(b => b.IsDelete == false)
            .Select(b => new BookViewModel(
                b.Id, b.Title, b.Author, b.Amount, b.ISBN, b.PublishYear))
            .ToListAsync();

        return Ok(books);
    }

    //[HttpGet("{id}")]
    //public IActionResult GetById(int id)
    //{
        

    //    if (book == null)
    //    {
    //        return NotFound(new { message = "Livro não encontrado." });
    //    }
    //    var book = _context.Books.FindAsync(id).Result;
        
    //     var bookViewModel = new BookViewModel(
    //        book.Id, book.Title, book.Author, book.Amount, book.ISBN, book.PublishYear);

    //        return Ok(bookViewModel);
   
    //}

    [HttpPost]//api/post
    public async Task<IActionResult> Create([FromBody] CreateBookInputModel model)
    {
        var book = new Books(
            model.Title,
            model.Author,
            1, // amount (ou model.Amount se tiver)
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

    [HttpPut("{id}")]
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

    [HttpPut("{id}/amount")]

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

    [HttpDelete("{id}")]
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

        return Ok($"O Livro nome: ({book.Title}) foi deletado"); // Código 204 - Sucesso sem retorno de conteúdo
    }


}
