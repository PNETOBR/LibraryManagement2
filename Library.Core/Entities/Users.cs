namespace LibraryManagement.Core.Entities;

public class Users : BaseEntity
{
    public Users()
    {
    }
    public Users(string name, string email, string password, DateTime birthday, int loanCount, bool Active = true)
        :base()
    {
        Name = name;
        Email = email;
        Password = password;
        Birthday = birthday;
        LoanCount = loanCount;
        Active = true;
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime Birthday { get; set; }
    public int LoanCount { get; set; }
    public ICollection<Loans> Loans { get; set; } = new List<Loans>();
    public bool Active { get; set; } = true;
}
