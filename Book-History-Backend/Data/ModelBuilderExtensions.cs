using Book_History_Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Book_History_Backend.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasData(
                new Author { AuthorId = 1, AuthorName = "William Shakespeare " },
                new Author { AuthorId = 2, AuthorName = "Dante Alighieri" },
                new Author { AuthorId = 3, AuthorName = "Sigmund Freud" }
                );
        }
    }
}
