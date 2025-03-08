using LibraryManagement.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{

    // GET: api/books
    [HttpGet]
    public IActionResult Get(string search = "")
    {
        return Ok("Books");
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(int id, CreateBookInputModel model)
    {
        throw new Exception();

        //return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }
}
