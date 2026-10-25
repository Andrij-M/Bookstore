using Microsoft.EntityFrameworkCore;
using Bookstore.Infrastructure.Entities1;

namespace Bookstore.Infrastructure.Entities1
{
    public class BookstoreDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<EBook> EBooks { get; set; }

        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publication>().ToTable("Publications");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<EBook>().ToTable("EBooks");
        }
    }
}