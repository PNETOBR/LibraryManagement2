using LibraryManagement.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Infraestructure;

public class LibraryManagementDbContext : DbContext
{
    public DbSet<Users> Users { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<Loans> Loans { get; set; }

    public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
        : base(options)
    {
    }
}
