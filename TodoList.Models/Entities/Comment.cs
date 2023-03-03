namespace TodoList.Models.Entities;

public sealed class Comment
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;
    
    public int TodoId { get; set; }
}