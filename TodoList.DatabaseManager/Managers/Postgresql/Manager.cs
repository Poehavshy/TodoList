using Microsoft.EntityFrameworkCore;
using TodoList.Models.Entities;
using TodoList.DatabaseManager.Interfaces;

namespace TodoList.DatabaseManager.Managers.Postgresql;

public class Manager : IDatabaseManager
{
    private readonly Context _context;

    public Manager(Context context)
    {
        _context = context;
    }

    public List<Todo> GetAllTodo()
    {
        return _context.Todos.Include(t => t.Comments).ToList();
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}