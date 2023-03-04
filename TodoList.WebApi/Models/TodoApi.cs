using TodoList.Models.Enums;

namespace TodoList.WebApi.Models;

public class TodoApi
{
    public string Title { get; set; } = null!;
    
    public Category Category { get; set; }
    
    public Color Color { get; set; }
}