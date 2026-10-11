namespace Bookstore.Common;

public abstract class Publication
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Author Author { get; set; } = new Author();

    public abstract void DisplayInfo();
}