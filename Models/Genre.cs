using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBookWebApi.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // N-N relationship
        public List<Book>? Books { get; set; }
    }
}