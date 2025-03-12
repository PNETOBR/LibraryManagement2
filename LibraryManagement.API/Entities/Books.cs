namespace LibraryManagement.API.Entities;

public class Books : BaseEntity
{
    public Books(string title, string author, int amount, int iSBN, string publisher) 

    {
        Title = title;
        Author = author;
        Amount = amount;
        ISBN = iSBN;
        Publisher = publisher;
    }

    public string Title { get; set; }
    public string Author { get; set; }
    public int Amount { get; set; }
    public int ISBN { get; set; }
    public string Publisher { get; set; }

}
