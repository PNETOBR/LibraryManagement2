using LibraryManagement.API.Entities;
using LibraryManagement.API.Infraestructure.Persistence;
using LibraryManagement.API.Model;
using LibraryManagement.API.ViewModels;
using LibraryManagement.API.ViewModels.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace LibraryManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly LibraryManagementDbContext _context;

    public UsersController(LibraryManagementDbContext context)
    {
        _context = context;
    }

    [HttpPost] //api/users
    public async Task<IActionResult> UserCreate([FromBody] CreateUserInputModel model)
    {
        var user = new Users(
            model.Name,
            model.Email,
            model.Password,
            model.Birthday,
            model.LoanCount
            );
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            return BadRequest(new { message = "Erro ao criar usuário", error = ex.Message });
        }

        return CreatedAtAction(nameof(GetAll), new { id = user.Id }, model);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int id = 0)
    {
        if (id > 0)
        {
            var user = _context.Users.FindAsync(id).Result;

            if (user != null)
            {
                var userViewModel = new UserViewModel(
                user.Id, user.Name, user.Email, user.LoanCount, user.Birthday);
                return Ok(userViewModel);
            }
            else
                return NotFound("Usuário Não Encontrado");
        }
        var users = await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .Where(u => u.Active == true)
            .Select(u => new UserViewModel(
                u.Id, u.Name, u.Email, u.LoanCount, u.Birthday))
            .ToListAsync();

        return Ok(users);
    }


    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound("Usuário não encontrado");

        user.Active = false;
        await _context.SaveChangesAsync();

        return Ok("Usuário deletado com sucesso");
    }

    [HttpPut("{id}/update-password")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordModel model)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        user.Password = model.NewPassword; 

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok("Senha alterada com sucesso.");
    }
}