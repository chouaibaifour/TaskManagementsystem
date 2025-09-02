using System.Text.Json;

namespace TaskManagement.Infrastructure.Data.Json;

public class FileStorage
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _options = new JsonSerializerOptions { WriteIndented = true };

    public FileStorage(string filePath)
    {
        _filePath = filePath;
        InitializeFile();
    }

    private void InitializeFile()
    {
        if (!File.Exists(_filePath)) File.WriteAllText(_filePath, "[]");
        
    }

    public async Task<List<T>> LoadAsync<T>()
    {
        await using var sr = File.OpenRead(_filePath);
        return await JsonSerializer.DeserializeAsync<List<T>>(sr) ?? new List<T>();
    }

    public async Task SaveAsync<T>(List<T> items)
    {
        await using var stream = File.Create(_filePath);
        await JsonSerializer.SerializeAsync(stream, items,
            _options);
    }
}