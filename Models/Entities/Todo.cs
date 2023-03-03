using Models.Enums;

namespace Models.Entities;

public sealed class Todo
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;
    
    public DateTime Created { get; set; }
    
    public bool IsDone { get; set; }
    
    public Category Category { get; set; }
    
    public Color Color { get; set; }
    
    public List<Comment>? Comments { get; set; }
}