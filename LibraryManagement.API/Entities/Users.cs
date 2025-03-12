namespace LibraryManagement.API.Entities;

public class Users : BaseEntity
{
    public Users(string name, string email, string password, DateTime birthday)
    {
        Name = name;
        Email = email;
        Password = password;
        Birthday = birthday;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime? Birthday { get; set; }
}
