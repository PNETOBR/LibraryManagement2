using LibraryManagement.Application.Models.DTOs.Inputs;
using LibraryManagement.Application.Models.DTOs.Outputs;
using LibraryManagement.Application.Models.Services.Interfaces;
using LibraryManagement.Core.Entities;
using LibraryManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models.Services;

public class UserService : IUserService
{
    private readonly LibraryManagementDbContext _context;

    public UserService(LibraryManagementDbContext context)
    {
        _context = context;
    }
    public async Task<UserViewModel> CreateUserAsync(CreateUserInputModel model)
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
            throw new Exception("Erro ao criar usuário: " + ex.Message);
        }

        return new UserViewModel(user.Id, user.Name, user.Email, user.LoanCount, user.Birthday, user.Active);
    }

    public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
    {
        var users = await _context.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            //.Where(u => u.Active)
            .Select(u => new UserViewModel(u.Id, u.Name, u.Email, u.LoanCount, u.Birthday, u.Active))
            .ToListAsync();

        return users;
    }

    public async Task<UserViewModel> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null) return null;

        return new UserViewModel(user.Id, user.Name, user.Email, user.LoanCount, user.Birthday, user.Active);
    }

    public async Task<string> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return "Usuário não encontrado";
        }

        user.Active = false;
        await _context.SaveChangesAsync();

        return "Usuário deletado com sucesso";
    }

    public async Task<string> UpdateUserPasswordAsync(int id, UpdatePasswordModel model)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return "Usuário não encontrado.";
        }

        user.Password = model.NewPassword;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return "Senha alterada com sucesso.";
    }
}
