using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LibraryManagement.API.Model;

public class CreateBookInputModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? ISBN { get; set; }
    public string Author { get; set; }
    public DateTime? YearOfPublication { get; set; }
}
