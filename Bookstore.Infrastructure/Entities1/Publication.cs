namespace Bookstore.Infrastructure.Entities1
{
    public abstract class Publication
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}