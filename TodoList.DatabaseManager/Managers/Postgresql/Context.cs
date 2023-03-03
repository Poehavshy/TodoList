using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.Enums;

namespace TodoList.DatabaseManager.Managers.Postgresql;

public sealed class Context : DbContext
{
    public DbSet<Todo> Todos { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;

    public Context(DbContextOptions<Context> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Todo>().HasIndex(t => new { t.Title, t.Category }).IsUnique();
        modelBuilder.Entity<Todo>().Property(t => t.Category).HasConversion<string>();
        modelBuilder.Entity<Todo>().Property(t => t.Color).HasConversion<string>();
        modelBuilder.Entity<Todo>().Property(t => t.Created).HasDefaultValueSql("now()");

        Todo todo1 = new()
        {
            Id = 1,
            Title = "Create a ticket",
            Category = Category.Analytics,
            Color = Color.Red
        };
        Todo todo2 = new()
        {
            Id = 2,
            Title = "Request information",
            Category = Category.Bookkeeping,
            Color = Color.Green
        };
        
        Comment comment1 = new()
        {
            Id = 1, 
            Text = "comment 1",
            TodoId = todo1.Id
        };
        Comment comment2 = new()
        {
            Id = 2, 
            Text = "comment 2",
            TodoId = todo1.Id
        };
        Comment comment3 = new() { 
            Id = 3, 
            Text = "comment 3",
            TodoId = todo1.Id
        };
        
        modelBuilder.Entity<Todo>().HasData(todo1, todo2);
        modelBuilder.Entity<Comment>().HasData(comment1, comment2, comment3);
    }
}