/*using Bookstore.Common;

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
*/

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Bookstore.Common;

namespace Bookstore.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Налаштування української локалізації в консолі
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("==================================================");
            Console.WriteLine(" Лабораторна робота №2: Багатопоковість, LINQ, Async");
            Console.WriteLine("==================================================\n");

            // Створюємо екземпляр нашого потокобезпечного асинхронного сервісу для класу Bus
            ICrudServiceAsync<Bus> busService = new CrudServiceAsync<Bus> { FilePath = "buses_storage.json" };

            Console.WriteLine("[1] Паралельне створення 1000 об'єктів Bus за допомогою Parallel.For...");
            var stopwatch = Stopwatch.StartNew();

            // Використання Parallel.For згідно з вимогою лабораторної
            Parallel.For(0, 1000, i =>
            {
                var bus = Bus.CreateNew();
                // Використовуємо асинхронний метод синхронно всередині паралельного циклу
                busService.CreateAsync(bus).Wait();
            });

            stopwatch.Stop();
            Console.WriteLine($"[+] Успішно створено та додано 1000 об'єктів за {stopwatch.ElapsedMilliseconds} мс.\n");

            // Демонстрація пагінації (наприклад, сторінка 1, по 5 елементів)
            Console.WriteLine("[2] Демонстрація пагінації (сторінка 1, 5 елементів):");
            var paginatedBuses = await busService.ReadAllAsync(1, 5);
            foreach (var b in paginatedBuses)
            {
                b.DisplayInfo();
            }
            Console.WriteLine();

            // Демонстрація LINQ запитів для обчислення мін, макс та середніх значень
            Console.WriteLine("[3] Аналіз даних за допомогою LINQ:");
            var allBuses = (await busService.ReadAllAsync()).ToList();

            if (allBuses.Any())
            {
                int minCapacity = allBuses.Min(b => b.Capacity);
                int maxCapacity = allBuses.Max(b => b.Capacity);
                double avgCapacity = allBuses.Average(b => b.Capacity);

                decimal minMileage = allBuses.Min(b => b.Mileage);
                decimal maxMileage = allBuses.Max(b => b.Mileage);
                decimal avgMileage = allBuses.Average(b => b.Mileage);

                Console.WriteLine($" - Місткість пасажирів: Мін = {minCapacity}, Макс = {maxCapacity}, Середня = {avgCapacity:F2}");
                Console.WriteLine($" - Пробіг (км): Мін = {minMileage:N2}, Макс = {maxMileage:N2}, Середній = {avgMileage:N2}");
            }
            Console.WriteLine();

            // Асинхронне збереження колекції у файл
            Console.WriteLine("[4] Збереження згенерованої колекції у файл (buses_storage.json)...");
            bool isSaved = await busService.SaveAsync();
            Console.WriteLine(isSaved ? "[+] Колекцію успішно збережено у файл!" : "[-] Помилка збереження файлу.");

            // Приклад використання примітиву синхронізації AutoResetEvent
            Console.WriteLine("\n[5] Демонстрація примітиву синхронізації (AutoResetEvent):");
            using (var autoEvent = new AutoResetEvent(false))
            {
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.WriteLine("    [Потік у фоні] Виконується фонова задача синхронізації...");
                    Thread.Sleep(500);
                    autoEvent.Set(); // Сигнал про завершення
                });

                autoEvent.WaitOne(); // Очікування сигналу
                Console.WriteLine("    [Головний потік] Форову задачу успішно завершено.");
            }

            Console.WriteLine("\n==================================================");
            Console.WriteLine(" Роботу програми завершено успішно!");
            Console.WriteLine("==================================================");
            Console.ReadKey();
        }
    }
}