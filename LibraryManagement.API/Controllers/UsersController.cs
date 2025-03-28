using LibraryManagement.API.Entities;
using LibraryManagement.API.Infraestructure.Persistence;
using LibraryManagement.API.Model;
using LibraryManagement.API.ViewModels.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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
            model.Birthday
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
                user.Id, user.Name, user.Email, user.Birthday);
                return Ok(userViewModel);
            }
            else
                return NotFound("Usuário Não Encontrado");
        }
        var users = await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .Where(u => u.IsDelete == false)
            .Select(u => new UserViewModel(
                u.Id, u.Name, u.Email, u.Birthday))
            .ToListAsync();

        return Ok(users);
    }
}