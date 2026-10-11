namespace Bookstore.Common;

public class EBook : Publication
{
    public double FileSizeMB { get; set; }
    public string Format { get; set; } = "PDF";

    public override void DisplayInfo()
    {
        Console.WriteLine($"[Е-Книга] ID: {Id} | Назва: \"{Title}\" | Автор: {Author.Name} | Формат: {Format} ({FileSizeMB} MB) | Ціна: {Price} грн");
    }
}