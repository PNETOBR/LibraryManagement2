using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Models.DTOs.Outputs;

public class UserViewModel
{
    public UserViewModel(int id, string name, string email, int loanCount, DateTime birthday, bool active)
    {
        Id = id;
        Name = name;
        Email = email;
        LoanCount = loanCount;
        Birthday = birthday;
        Active = active;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int LoanCount { get; set; }
    public DateTime? Birthday { get; set; }
    public bool Active { get; set; }

    public static UserViewModel FromEntity(Users entity)
        => new(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.LoanCount,
            entity.Birthday,
            entity.Active
        );

}
