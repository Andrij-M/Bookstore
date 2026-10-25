namespace Bookstore.Infrastructure.Entities1
{
    public class Book : Publication
    {
        public int PagesCount { get; set; }
        public string CoverType { get; set; }
    }
}