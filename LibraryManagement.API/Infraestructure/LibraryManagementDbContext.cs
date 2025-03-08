using LibraryManagement.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Infraestructure;

public class LibraryManagementDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\PNETO\\Desktop\\TechLibraryDb.db");
    }
}
