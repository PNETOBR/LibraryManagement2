using LibraryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Infrastructure.Persistence;

public class LibraryManagementDbContext : DbContext
{
    public LibraryManagementDbContext(DbContextOptions<LibraryManagementDbContext> options)
        : base(options) { }

    public DbSet<Users> Users { get; set; }
    public DbSet<Books> Books { get; set; }
    public DbSet<Loans> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Chave Primária das entidades
        builder.Entity<Users>().HasKey(u => u.Id);
        builder.Entity<Books>().HasKey(b => b.Id);
        builder.Entity<Loans>().HasKey(l => l.Id);

        // Relacionamento entre Loan e User
        builder.Entity<Loans>()
            .HasOne(l => l.User)  // Um empréstimo pertence a um usuário
            .WithMany(u => u.Loans) // Um usuário pode ter vários empréstimos
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento entre Loan e Book
        builder.Entity<Loans>()
            .HasOne(l => l.Books) // Um empréstimo pertence a um livro
            .WithMany(b => b.Loans) // Um livro pode estar em vários empréstimos
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
