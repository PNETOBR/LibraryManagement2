﻿using LibraryManagement.API.Entities;

namespace LibraryManagement.API.ViewModels.Views;

public class UserViewModel
{
    public UserViewModel(int id, string name, string email, DateTime birthday)
    {
        Id = id;
        Name = name;
        Email = email;
        Birthday = birthday;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    //public List<Loans> Loans { get; set; }
    public DateTime? Birthday { get; set; }

    public static UserViewModel FromEntity(Users entity)
        => new(
            entity.Id,
            entity.Name,
            entity.Email,
            entity.Birthday
        );

}
