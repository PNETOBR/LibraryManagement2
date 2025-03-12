namespace LibraryManagement.API.Entities;

public class Loans : BaseEntity
{
    public Loans(int userId, int bookId, DateTime loanDate, DateTime returnDate, bool returned)
    {

        UserId = userId;
        BookId = bookId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Returned = false;
    }

    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool Returned { get; set; }

}
