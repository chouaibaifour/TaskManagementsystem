using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskManagement.Infrastructure.Data.Json
{
    public class FileStorage
    {
        private readonly string _filePath;



        public FileStorage(string filePath)
        {
            _filePath = filePath;
            InitializeFile();

        }
        private void InitializeFile()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, "[]");
            }
        }

        public async Task<List<T>> LoadAsync<T>()
        {
            using var sr = File.OpenRead(_filePath);
            return await JsonSerializer.DeserializeAsync<List<T>>(sr) ?? new List<T>();
        }
        public async Task SaveAsync<T>(List<T> items)
        {
            using var stream = File.Create(_filePath);
            await JsonSerializer.SerializeAsync(stream, items,
                new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
