namespace LibraryManagement.API.Entities;

public class Books : BaseEntity
{
    public Books(string title, string author, int amount, int iSBN, string publishYear)
        :base()

    {
        Title = title;
        Author = author;
        Amount = amount;
        ISBN = iSBN;
        PublishYear = publishYear;
    }

    public string Title { get; set; }
    public string Author { get; set; }
    public int Amount { get; set; }
    public int ISBN { get; set; }
    public string PublishYear { get; set; }
    public List<Loans> Loans { get; set; }

}
