using System.Collections.Generic;

namespace Bookstore.Infrastructure.Entities1
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Publication> Publications { get; set; } = new List<Publication>();
    }
}