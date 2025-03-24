using LibraryManagement.API.Entities;

namespace LibraryManagement.API.ViewModels.Views;

public class LoanViewModel
{
    public LoanViewModel(int id, int userId, int bookId, Books title, DateTime loanDate, DateTime returnDate, bool returned)
    {
        Id = id;
        UserId = userId;
        BookId = bookId;
        Title = title;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Returned = returned;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public Books Title { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool Returned { get; set; }

}
