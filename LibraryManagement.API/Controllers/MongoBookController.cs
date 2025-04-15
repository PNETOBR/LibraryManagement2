using LibraryManagement.API.Infraestructure.MongoDB.Services;
using LibraryManagement.API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

//[Route("api/mongo-books")]
//[ApiController]
//public class MongoBookController : ControllerBase
//{
//    private readonly MongoBookService _mongoBookService;

//    public MongoBookController(MongoBookService mongoBookService)
//    {
//        _mongoBookService = mongoBookService ?? throw new ArgumentNullException(nameof(mongoBookService));
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetBooks()
//    {
//        var books = await _mongoBookService.GetAllBooksAsync();
//        return Ok(books);
//    }

//    [HttpPost]
//    public async Task<IActionResult> CreateBook(MongoBook book)
//    {
//        await _mongoBookService.AddBookAsync(book);
//        return CreatedAtAction(nameof(GetBooks), new { id = book.Id }, book);
//    }
//}
