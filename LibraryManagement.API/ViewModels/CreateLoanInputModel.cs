namespace LibraryManagement.API.Model;

public class CreateLoanInputModel
{
    public CreateLoanInputModel(int id, int userId, int bookId, DateTime loanDate)
    {
        Id = id;
        UserId = userId;
        BookId = bookId;
        LoanDate = loanDate;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
}
