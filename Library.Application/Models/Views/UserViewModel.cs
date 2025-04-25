using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Models.Views;

public class UserViewModel
{
    public UserViewModel(int id, string name, string email,int loanCount, DateTime birthday)
    {
        Id = id;
        Name = name;
        Email = email;
        LoanCount = loanCount;
        Birthday = birthday;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int LoanCount { get; set; }
    public DateTime? Birthday { get; set; }

    public static UserViewModel FromEntity(Users entity)
        => new(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.LoanCount,
            entity.Birthday
        );

}
