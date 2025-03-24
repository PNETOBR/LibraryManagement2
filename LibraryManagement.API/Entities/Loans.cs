namespace LibraryManagement.API.Entities;

public class Loans : BaseEntity
{
    public Loans()
    {
    }
    public Loans(int userId, int bookId, DateTime loanDate, DateTime returnDate, bool returned)
        :base()
    {

        UserId = userId;
        BookId = bookId;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Returned = false;
    }

    public int UserId { get; set; }
    public Users User { get; set; }
    public int BookId { get; set; }
    public Books Books { get; set; }
    public DateTime? LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public bool Returned { get; set; }

}
