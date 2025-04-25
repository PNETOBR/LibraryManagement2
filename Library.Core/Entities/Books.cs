using System.Text.Json.Serialization;

namespace LibraryManagement.Core.Entities;

public class Books : BaseEntity
{
    public Books()
    {
    }
    public Books(string title, string author, int amount, int iSBN, string publishYear, bool isDelete = false)
        :base()

    {
        Title = title;
        Author = author;
        Amount = amount;
        ISBN = iSBN;
        PublishYear = publishYear;
        IsDelete = isDelete;

    }

    public string Title { get; set; }
    public string Author { get; set; }
    public int Amount { get; set; }
    public int ISBN { get; set; }
    public string PublishYear { get; set; }

    [JsonIgnore]
    public List<Loans>? Loans { get; set; }

    [JsonIgnore]
    public bool IsDelete { get; set; }

}
