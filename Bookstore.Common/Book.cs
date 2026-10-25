namespace Bookstore.Common;

public class Book : Publication
{
    public int PageCount { get; set; }

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Книга] ID: {Id} | Назва: \"{Title}\" | Автор: {Author.Name} | Сторінок: {PageCount} | Ціна: {Price} грн");
    }
}