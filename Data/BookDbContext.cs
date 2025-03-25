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
    }
}