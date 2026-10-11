using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Bookstore.Common
{
    public class CrudServiceAsync<T> : ICrudServiceAsync<T> where T : class
    {
        // Thread-safe колекція для багатопокового середовища
        private readonly ConcurrentDictionary<Guid, T> _storage = new ConcurrentDictionary<Guid, T>();

        // Примітиви синхронізації згідно з вимогою лабораторної
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly object _lockObject = new object();

        public string FilePath { get; set; } = "storage_data.json";

        public async Task<bool> CreateAsync(T element)
        {
            if (element == null) return false;

            // Отримуємо властивість Id через рефлексію або припускаємо наявність Id
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null) return false;

            if (idProperty.GetValue(element) is Guid id)
            {
                // Використовуємо SemaphoreSlim для демонстрації асинхронної синхронізації
                await _semaphore.WaitAsync();
                try
                {
                    return _storage.TryAdd(id, element);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            return false;
        }

        public async Task<T> ReadAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                _storage.TryGetValue(id, out var element);
                return element;
            });
        }

        public async Task<IEnumerable<T>> ReadAllAsync()
        {
            return await Task.FromResult(_storage.Values.ToList());
        }

        // Реалізація пагінації згідно із завданням
        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            return await Task.Run(() =>
            {
                if (page < 1) page = 1;
                if (amount < 1) amount = 10;

                return _storage.Values
                    .Skip((page - 1) * amount)
                    .Take(amount)
                    .ToList();
            });
        }

        public async Task<bool> UpdateAsync(T element)
        {
            if (element == null) return false;
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty?.GetValue(element) is Guid id)
            {
                await _semaphore.WaitAsync();
                try
                {
                    if (_storage.ContainsKey(id))
                    {
                        _storage[id] = element;
                        return true;
                    }
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            return false;
        }

        public async Task<bool> RemoveAsync(T element)
        {
            if (element == null) return false;
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty?.GetValue(element) is Guid id)
            {
                await _semaphore.WaitAsync();
                try
                {
                    return _storage.TryRemove(id, out _);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
            return false;
        }

        public async Task<bool> SaveAsync()
        {
            // Демонстрація використання синхронного lock у парі з асинхронним потоком
            lock (_lockObject)
            {
                try
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    var jsonString = JsonSerializer.Serialize(_storage.Values, options);
                    File.WriteAllText(FilePath, jsonString);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _storage.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}