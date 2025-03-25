using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleBookWebApi.Models;

namespace SimpleBookWebApi.Data
{
    public class BookDbContext(DbContextOptions<BookDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set;}
        public DbSet<Book> BooksDetails { get; set;}
        public DbSet<Book> Authors { get; set;}
        public DbSet<Book> Genres { get; set;}


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<Task>()
                .HasOne(e => e.TaskDetail)
                .WithOne(e => e.Task)
                .HasForeignKey<TaskDetail>(e => e.TaskId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);*/  

            // Book and Genre (many to many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Genres)
                .WithMany(g => g.Books);

            // Authors and Books (one to many)
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany()
                .HasForeignKey(b => b.AuthorId);

            // Authors
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "J.K. Rowling" },
                new Author { Id = 2, Name = "George R.R. Martin" },
                new Author { Id = 3, Name = "J.R.R. Tolkien" },
                new Author { Id = 4, Name = "Harper Lee" },
                new Author { Id = 5, Name = "F. Scott Fitzgerald" }
            );

            // Genres
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fantasy" },
                new Genre { Id = 2, Name = "Drama" },
                new Genre { Id = 3, Name = "Adventure" },
                new Genre { Id = 4, Name = "Classic" },
                new Genre { Id = 5, Name = "Mystery" }
            );

            // Books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 1 },
                new Book { Id = 2, Title = "A Game of Thrones", AuthorId = 2 },
                new Book { Id = 3, Title = "The Hobbit", AuthorId = 3 },
                new Book { Id = 4, Title = "To Kill a Mockingbird", AuthorId = 4 },
                new Book { Id = 5, Title = "The Great Gatsby", AuthorId = 5 }
            );

            // BookDetails
            modelBuilder.Entity<BookDetails>().HasData(
            new BookDetails { Id = 1, BookId = 1, Description = "A young wizard begins his magical journey.", PublishedDate = new DateTime(1997, 6, 26, 0, 0, 0, DateTimeKind.Utc), TotalPages = 309 },
            new BookDetails { Id = 2, BookId = 2, Description = "Noble families vie for control of the Iron Throne.", PublishedDate = new DateTime(1996, 8, 6, 0, 0, 0, DateTimeKind.Utc), TotalPages = 694 },
            new BookDetails { Id = 3, BookId = 3, Description = "A hobbit sets out on a perilous quest to reclaim treasure.", PublishedDate = new DateTime(1937, 9, 21, 0, 0, 0, DateTimeKind.Utc), TotalPages = 310 },
            new BookDetails { Id = 4, BookId = 4, Description = "A deep exploration of racism and justice.", PublishedDate = new DateTime(1960, 7, 11, 0, 0, 0, DateTimeKind.Utc), TotalPages = 281 },
            new BookDetails { Id = 5, BookId = 5, Description = "A mysterious millionaire and the American dream.", PublishedDate = new DateTime(1925, 4, 10, 0, 0, 0, DateTimeKind.Utc), TotalPages = 180 }
            );

            // Book-Genre many-to-many relationships
            /*modelBuilder.Entity<Book>()
            .HasData(
                new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 1, Genres = new List<Genre> { new Genre { Id = 1, Name = "Fantasy" }, new Genre { Id = 3, Name = "Adventure" } } },
                new Book { Id = 2, Title = "A Game of Thrones", AuthorId = 2, Genres = new List<Genre> { new Genre { Id = 1, Name = "Fantasy" }, new Genre { Id = 3, Name = "Adventure" } } },
                new Book { Id = 3, Title = "The Hobbit", AuthorId = 3, Genres = new List<Genre> { new Genre { Id = 1, Name = "Fantasy" }, new Genre { Id = 3, Name = "Adventure" } } },
                new Book { Id = 4, Title = "To Kill a Mockingbird", AuthorId = 4, Genres = new List<Genre> { new Genre { Id = 2, Name = "Drama" }, new Genre { Id = 4, Name = "Classic" } } },
                new Book { Id = 5, Title = "The Great Gatsby", AuthorId = 5, Genres = new List<Genre> { new Genre { Id = 2, Name = "Drama" }, new Genre { Id = 4, Name = "Classic" } } }
            );*/
        }

    }
}