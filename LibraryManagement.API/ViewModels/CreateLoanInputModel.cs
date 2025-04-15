namespace LibraryManagement.API.Model;

public class CreateLoanInputModel
{

    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; }
}
