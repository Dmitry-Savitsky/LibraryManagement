using Microsoft.EntityFrameworkCore;
using LibraryManagement.Core.Entities;
using System.Collections.Generic;

namespace LibraryManagement.Infrastructure.Persistence
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookHasUser> BookHasUsers { get; set; }
        public DbSet<BookCharacteristics> BookCharacteristics { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookHasUser>()
                .HasKey(bhu => new { bhu.BookId, bhu.UserId });

            modelBuilder.Entity<BookHasUser>()
                .HasOne(bhu => bhu.Book)
                .WithMany(b => b.BorrowedBy)
                .HasForeignKey(bhu => bhu.BookId);

            modelBuilder.Entity<BookHasUser>()
                .HasOne(bhu => bhu.User)
                .WithMany(u => u.BorrowedBooks)
                .HasForeignKey(bhu => bhu.UserId);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.BookCharacteristics)
                .WithMany()
                .HasForeignKey(b => b.BookCharacteristicsId);

            modelBuilder.Entity<BookCharacteristics>()
                .HasOne(bc => bc.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(bc => bc.AuthorId);
        }
    }
}
