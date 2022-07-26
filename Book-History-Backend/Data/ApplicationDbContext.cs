using Book_History_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_History_Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()
                .HasKey(ba => new { ba.Id, ba.AuthorId });
            modelBuilder.Entity<AuthorBook>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(bc => bc.Id);
            modelBuilder.Entity<AuthorBook>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.AuthorBooks)
                .HasForeignKey(bc => bc.AuthorId);

            modelBuilder.Seed();
        }


    }
}
