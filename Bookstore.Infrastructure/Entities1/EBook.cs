namespace Bookstore.Infrastructure.Entities1
{
    public class EBook : Publication
    {
        public double FileSizeMB { get; set; }
        public string Format { get; set; }
    }
}