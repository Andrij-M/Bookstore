namespace Bookstore.Common;

public static class PublicationExtensions
{
    public static void PrintWithDiscount(this Publication publication, decimal discountPercentage)
    {
        decimal discountedPrice = publication.Price * (1 - discountPercentage / 100);
        Console.WriteLine($"[Знижка {discountPercentage}%] \"{publication.Title}\" -> Нова ціна: {discountedPrice:F2} грн (Стара: {publication.Price} грн)");
    }
}