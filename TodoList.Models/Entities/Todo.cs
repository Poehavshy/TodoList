using System.Security.Cryptography;
using TodoList.Models.Enums;

namespace TodoList.Models.Entities;

public sealed class Todo
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    
    public DateTime Created { get; set; }
    
    public bool IsDone { get; set; }
    
    public Category Category { get; set; }
    
    public Color Color { get; set; }
    
    public List<Comment>? Comments { get; set; }

    public string? Hash { get; set; } = _hash?.Value;

    private static Lazy<string>? _hash;

    public Todo()
    {
        _hash = new Lazy<string>(() =>
            Convert.ToHexString(MD5.HashData(System.Text.Encoding.ASCII.GetBytes(Title))));
    }
}