using LibraryManagement.API.Entities;

namespace LibraryManagement.API.ViewModels.Views;

public class BookViewModel
{
    public BookViewModel(int id, string title, string author, int amount, int iSBN, string publisher)
    {
        Id = id;
        Title = title;
        Author = author;
        Amount = amount;
        ISBN = iSBN;
        Publisher = publisher;

    }
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Amount { get; private set; }
    public int ISBN { get; private set; }
    public string Publisher { get; private set; }

    public static BookViewModel FromEntity(Books entity)
        => new(
            entity.Id,
            entity.Title,
            entity.Author,
            entity.Amount,
            entity.ISBN,
            entity.PublishYear
        );
}
