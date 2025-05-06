using LibraryManagement.Application.Models.DTOs.Inputs;
using LibraryManagement.Application.Models.DTOs.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Application.Models.Services.Interfaces;

public interface IUserService 
{
    Task<UserViewModel> CreateUserAsync(CreateUserInputModel model);
    Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    Task<UserViewModel> GetUserByIdAsync(int id);
    Task<string> DeleteUserAsync(int id);
    Task<string> UpdateUserPasswordAsync(int id, UpdatePasswordModel model);
}
