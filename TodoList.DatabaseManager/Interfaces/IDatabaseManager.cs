using TodoList.Models.Entities;

namespace TodoList.DatabaseManager.Interfaces;

public interface IDatabaseManager : IDisposable
{
    List<Todo> GetAllTodo();
    void SaveChanges();
}