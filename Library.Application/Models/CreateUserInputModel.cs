namespace LibraryManagement.Application.Models;

public class CreateUserInputModel
{
    public CreateUserInputModel(string name, DateTime birthday, string email, string password)
    {

        Name = name;
        Birthday = birthday;
        Email = email;
        Password = password;
    }

    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Loans { get; set; }
    public int LoanCount { get; set; }
}
