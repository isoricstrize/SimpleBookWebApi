using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SimpleBookWebApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        // N-N relationship
        [JsonIgnore] // we want to see Id and Name, but ignore the books -> break the circural relationship
        public List<Book>? Books { get; set; }
    }
}