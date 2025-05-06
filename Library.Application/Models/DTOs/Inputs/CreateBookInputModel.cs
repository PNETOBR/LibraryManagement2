namespace LibraryManagement.Application.Models.DTOs.Inputs;

public class CreateBookInputModel
{
    public string Title { get; set; }
    public int ISBN { get; set; }
    public string Author { get; set; }
    public DateTime? YearOfPublication { get; set; }

}


