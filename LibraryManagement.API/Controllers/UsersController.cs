using LibraryManagement.Application.Models.DTOs.Inputs;
using LibraryManagement.Application.Models.DTOs.Outputs;
using LibraryManagement.Application.Models.DTOs.Result;
using LibraryManagement.Application.Models.Services.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost] // api/users
    public async Task<IActionResult> UserCreate([FromBody] CreateUserInputModel model)
    {
        try
        {
            var userViewModel = await _userService.CreateUserAsync(model);
            return CreatedAtAction(nameof(GetAll), new { id = userViewModel.Id }, userViewModel);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = "Erro ao criar usuário", error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int id = 0)
    {
        if (id > 0)
        {
            var userViewModel = await _userService.GetUserByIdAsync(id);
            if (userViewModel != null)
            {
                return Ok(userViewModel);
            }
            else
                return NotFound("Usuário Não Encontrado");
        }

        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var message = await _userService.DeleteUserAsync(id);
        if (message.Contains("não encontrado"))
            return NotFound(message);

        return Ok(message);
    }

    [HttpPut("{id}/update-password")]
    public async Task<IActionResult> UpdatePassword(int id, [FromBody] UpdatePasswordModel model)
    {
        var message = await _userService.UpdateUserPasswordAsync(id, model);
        if (message.Contains("não encontrado"))
            return NotFound(message);

        return Ok(message);
    }
}