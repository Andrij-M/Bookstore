using System;

namespace Bookstore.Common
{
    public class Bus
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Model { get; set; }
        public int Capacity { get; set; }      // Місткість пасажирів (для числових розрахунків)
        public decimal Mileage { get; set; }   // Пробіг (для числових розрахунків)

        // Статичний метод для генерації випадкового об'єкта Bus
        public static Bus CreateNew()
        {
            var random = new Random();
            string[] models = { "Mercedes-Benz", "Volvo", "MAN", "Богдан", "Еталон", "Scania", "Ikarus" };

            return new Bus
            {
                Model = models[random.Next(models.Length)],
                Capacity = random.Next(18, 65),
                Mileage = Math.Round((decimal)(random.NextDouble() * 400000), 2)
            };
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Bus ID: {Id}, Model: {Model}, Capacity: {Capacity}, Mileage: {Mileage} km");
        }
    }
}