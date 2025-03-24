using LibraryManagement.API.Entities;

namespace LibraryManagement.API.ViewModels.Views;

public class UserViewModel
{
    public UserViewModel(int id, string name, string email, List<Loans> loans)
    {
        Id = id;
        Name = name;
        Email = email;
        Loans = loans;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Loans> Loans { get; set; }
}
