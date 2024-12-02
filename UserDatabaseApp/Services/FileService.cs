using System.Diagnostics;
using System.Net;
using System.Text.Json;
using UserDatabaseApp.Models;

namespace UserDatabaseApp.Services;

public class FileService
{
    private readonly string _directoryPath;
    private readonly string _filePath;

    private readonly JsonSerializerOptions _JsonSerializerOptions;

    public FileService(string directoryPath = "Data", string fileName = "list.json")
    {
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
        _JsonSerializerOptions = new JsonSerializerOptions { WriteIndented = true };
    }

    public void SaveListToFile(List<User> list)
    {
        try
        {
            if (!Directory.Exists(_directoryPath))
            {
                Directory.CreateDirectory(_directoryPath);
            }

            var json = JsonSerializer.Serialize(list, _JsonSerializerOptions);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public List<User> LoadListFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return new List<User>();
            }

            var json = File.ReadAllText(_filePath);
            var list = JsonSerializer.Deserialize<List<User>>(json, _JsonSerializerOptions);
            return list ?? [];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return new List<User>();
        }
    }

    public void DeleteUser(string id)
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                return;
            }

            var list = LoadListFromFile();
            list.RemoveAll(user => user.Id == id);
            SaveListToFile(list);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
}
