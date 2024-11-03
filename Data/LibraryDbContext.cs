using System.Data.Entity; // Make sure to include this namespace
using u21432865_HW03.Models; // Adjust this according to your project's namespace

namespace u21432865_HW03.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() : base("DefaultConnection") // This uses the connection string from web.config
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookType> BookTypes { get; set; }

        public System.Data.Entity.DbSet<u21432865_HW03.Models.Borrow> Borrows { get; set; }

        // Add other DbSets as needed
    }
}
