using System.Collections.Generic;
using System;
using System.Data.Entity;
using u21432865_HW03.Models; // Make sure to replace this with your actual namespace

public class LibraryDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookType> BookTypes { get; set; } // Add this line
    public DbSet<Borrow> Borrows { get; set; }

    public LibraryDbContext() : base("LibraryConnectionString")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        // Map BookType to the Types table
        modelBuilder.Entity<BookType>().ToTable("Types");

        // Explicitly define primary key if needed
        modelBuilder.Entity<BookType>().HasKey(bt => bt.TypeId);

        base.OnModelCreating(modelBuilder);
    }

}

