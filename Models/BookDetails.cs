using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookWebApi.Models
{
    public class BookDetails
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime PublishedDate { get; set; }
        public int TotalPages { get; set; }

        // 1-1 relationship
        public int BookId { get; set; } 
    }
}