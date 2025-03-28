using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleBookWebApi.Data;
using SimpleBookWebApi.Models;

namespace SimpleBookWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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