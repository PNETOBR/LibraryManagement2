using LibraryManagement.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LibraryManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly LoansBookLimitedConfig _config;
    public LoansController(IOptions<LoansBookLimitedConfig> options)
    {
        _config = options.Value;
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Post(CreateLoanInputModel model)
    {
        int currentLoans = 7;
        if (currentLoans >= _config.MaxLoansForPerson)
        {
            return BadRequest($"O usuário atingiu o limite máximo de {_config.MaxLoansForPerson} empréstimos.");
        }
        return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
    }

    [HttpPut("{id}")]
    public IActionResult LoanReturn(int id)
    {
        return Ok();
    }


}
