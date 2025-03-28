namespace LibraryManagement.API.Entities;

public class Users : BaseEntity
{
    public Users()
    {
    }
    public Users(string name, string email, string password, DateTime birthday, bool isDelete = false)
        :base()
    {
        Name = name;
        Email = email;
        Password = password;
        Birthday = birthday;
        IsDelete = false;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Birthday { get; set; }
    public List<Loans> Loans { get; set; }
    public bool IsDelete { get; set; }
}
