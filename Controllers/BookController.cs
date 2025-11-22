using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBookWebApi.Data;
using SimpleBookWebApi.Entities;

namespace SimpleBookWebApi.Controllers
{
    // -----------------------------------------------------------------------------
    // BookController
    //
    // Provides CRUD operations for managing books.  
    // All endpoints require authentication and write operations  
    // (create, update, delete) are restricted to users with the Admin role.
    // -----------------------------------------------------------------------------

    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // <- all actions require authentication
    public class BookController(BookDbContext db) : ControllerBase
    {
        private readonly BookDbContext _db = db;

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetBooks()
        {
            return Ok(await _db.Books
                .Include(b => b.BookDetails)
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBookById(int id)
        {
            var book = await _db.Books.FindAsync(id);

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> AddNewBook(Book newBook)
        {
            if (newBook is null)
                return BadRequest();

            _db.Books.Add(newBook);
            await _db.SaveChangesAsync();

            //return Created();
            return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, Book updatedBook)
        {
            var book = await _db.Books.FindAsync(id);

            if (book is null)
                return NotFound();

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genres = updatedBook.Genres;
            book.BookDetails = updatedBook.BookDetails;

            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _db.Books.FindAsync(id);

            if (book is null)
                return NotFound();

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();

            return NoContent();
        }

    }
}