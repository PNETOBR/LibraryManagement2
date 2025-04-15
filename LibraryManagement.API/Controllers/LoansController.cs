using LibraryManagement.API.Entities;
using LibraryManagement.API.Infraestructure.Persistence;
using LibraryManagement.API.Model;
using LibraryManagement.API.ViewModels.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LibraryManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{

    private readonly LibraryManagementDbContext _context;
    private readonly LoansBookLimitedConfig _config;

    public LoansController(LibraryManagementDbContext context, IOptions<LoansBookLimitedConfig> options)
    {
        _context = context;
        _config = options.Value;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll(int id = 0)
    {
        if(id > 0)
        {
            var loan = _context.Loans.FindAsync(id).Result;

            if (loan != null)
            {
                var loanViewModel = new LoanViewModel(
                    loan.Id,
                    loan.UserId,
                    loan.User?.Name ?? "Desconhecido",
                    loan.BookId,
                    loan.Books,
                    loan.LoanDate ?? DateTime.Now,
                    loan.ReturnDate ?? DateTime.Now,
                    loan.Returned
                );
                return Ok(loanViewModel);
            }
            else
                return NotFound("Empréstimo Não Encontrado");
        }
        var loans = await _context.Loans
            .Include(l => l.Books)
            .Include(l => l.User)
            .Where(l => !l.Returned)
            .OrderBy(l => l.Id)
            .ToListAsync();

        var vieModels = loans.Select(LoanViewModel.FromEntity).ToList();

        return Ok(vieModels);
    }


    [HttpPost]
    public async Task<IActionResult> Post(CreateLoanInputModel model)
    {
        int currentLoans = await _context.Loans
            .CountAsync(l => l.UserId == model.UserId && !l.Returned);

        if (currentLoans >= _config.MaxLoansForPerson)
        {
            return BadRequest($"O usuário atingiu o limite máximo de empréstimos.");
        }

        var loan = new Loans(
            model.UserId,
            model.BookId,
            model.LoanDate,
            model.LoanDate.AddDays(7), // ou outra regra de negócio para returnDate
            false
        );

        await _context.Loans.AddAsync(loan);

        // Verifica se o usuário existe
        var user = await _context.Users.FindAsync(model.UserId);
        if (user == null) return NotFound("Usuário não encontrado");

        // Verifica se o livro existe
        var book = await _context.Books.FindAsync(model.BookId);
        if (book == null) return NotFound("Livro não encontrado");

        // Atualiza os dados
        book.Amount--;
        user.LoanCount++;

        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = loan.Id }, LoanViewModel.FromEntity(loan));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> LoanReturn(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.Books)
            .Include(l => l.User)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (loan == null)
            return NotFound("Empréstimo não encontrado");

        if (loan.Returned)
            return BadRequest("Este empréstimo já foi devolvido");

        var now = DateTime.UtcNow;

        // Validação de atraso
        bool isLate = loan.ReturnDate != null && now > loan.ReturnDate.Value;

        loan.Returned = true;

        if (loan.Books != null)
            loan.Books.Amount++;

        if (loan.User != null && loan.User.LoanCount > 0)
            loan.User.LoanCount--;

        await _context.SaveChangesAsync();

        string message = isLate ? "Livro devolvido com atraso. \n Pague R$2,00" : "Livro devolvido no prazo.";
        return Ok(message);
    }


}
