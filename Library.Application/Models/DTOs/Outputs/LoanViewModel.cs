﻿using LibraryManagement.Core.Entities;

namespace LibraryManagement.Application.Models.DTOs.Outputs;

public class LoanViewModel
{
    public LoanViewModel(int id, int userId, string userName, int bookId, Books title, DateTime loanDate, DateTime returnDate, bool returned = false)
    {
        Id = id;
        UserId = userId;
        UserName = userName;
        BookId = bookId;
        Title = title;
        LoanDate = loanDate;
        ReturnDate = returnDate;
        Returned = returned;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int BookId { get; set; }
    public Books Title { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public bool Returned { get; set; }


    public static LoanViewModel FromEntity(Loans entity)
        => new(
            entity.Id,
            entity.UserId,
            entity.User?.Name ?? "Desconhecido",
            entity.BookId,
            entity.Books,
            entity.LoanDate ?? DateTime.Now,
            entity.ReturnDate ?? DateTime.Now,
            entity.Returned
        );
}
