using Bookstore.Common;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("==================================================");
Console.WriteLine("   IT-проєкт: Система управління книжковим магазином");
Console.WriteLine("==================================================\n");

var bookService = new CrudService<Publication>();

var author1 = new Author { Id = 1, Name = "Тарас Шевченко" };
var author2 = new Author { Id = 2, Name = "Джордж Орвелл" };

bookService.Add(new Book { Id = 1, Title = "Кобзар", Price = 350.00m, PageCount = 720, Author = author1 });
bookService.Add(new EBook { Id = 2, Title = "1984", Price = 180.00m, FileSizeMB = 4.5, Format = "EPUB", Author = author2 });

Console.WriteLine("--- 1. Список усіх публікацій (READ) ---");
foreach (var item in bookService.GetAll())
{
    item.DisplayInfo();
}

Console.WriteLine("\n--- 2. Демонстрація Extension-методу (Знижки) ---");
var firstBook = bookService.GetById(1);
firstBook?.PrintWithDiscount(15);

Console.WriteLine("\n--- 3. Оновлення даних (UPDATE) ---");
bookService.Update(2, new EBook { Id = 2, Title = "1984 (Спеціальне видання)", Price = 210.00m, FileSizeMB = 5.0, Format = "PDF", Author = author2 });
Console.WriteLine("Оновлено книгу з ID 2.");

Console.WriteLine("\n--- 4. Перевірка після оновлення ---");
foreach (var item in bookService.GetAll())
{
    item.DisplayInfo();
}

Console.WriteLine("\n--- 5. Видалення (DELETE) ---");
bookService.Delete(1);
Console.WriteLine("Видалено книгу з ID 1.");

Console.WriteLine("\n--- 6. Підсумковий список ---");
foreach (var item in bookService.GetAll())
{
    item.DisplayInfo();
}

Console.WriteLine("\n==================================================");
Console.WriteLine("   Роботу програми успішно завершено!");
Console.WriteLine("==================================================");