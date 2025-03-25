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

    }
}