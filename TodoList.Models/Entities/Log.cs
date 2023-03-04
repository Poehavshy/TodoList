namespace TodoList.Models.Entities;

public class Log
{
    public int Id { get; set; }

    public string Application { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string Message { get; set; } = null!;
    
    public string Logger { get; set; } = null!;
    
    public string Callsite { get; set; } = null!;
    
    public string Exception { get; set; } = null!;
    
    public string Logged { get; set; } = null!;
}