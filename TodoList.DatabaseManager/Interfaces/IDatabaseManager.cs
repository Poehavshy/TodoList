using TodoList.Models.Entities;

namespace TodoList.DatabaseManager.Interfaces;

public interface IDatabaseManager : IDisposable
{
    Task<List<Todo>> GetAllTodo(bool includes = false);

    Task<Todo?> GetTodo(int id, bool includes = false);

    Task<bool> CheckTodoUnique(Todo todo);

    Task AddTodo(Todo todo);

    Task DeleteTodo(int id);

    Task UpdateTodo(Todo todo);
    
    Task AddComment(Comment comment);
    
    Task SaveChanges();
}