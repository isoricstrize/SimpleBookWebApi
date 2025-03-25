using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookWebApi.Models
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }

        // 1-1 relationship
        public BookDetails? BookDetails { get; set; }

        // 1-N relationship
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }

        // N-N relationship
        public List<Genre>? Genres { get; set; }
    }
}