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

    public async Task<List<Todo>> GetAllTodo(bool includes = false)
    {
        if (includes)
        {
            return await _context.Todos.Include(t => t.Comments).ToListAsync();
        }
        return await _context.Todos.ToListAsync();
    }

    public async Task<Todo?> GetTodo(int id, bool includes = false)
    {
        if (includes)
        {
            return await _context.Todos.Include(t => t.Comments).FirstOrDefaultAsync(t => t.Id == id);
        }
        return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> CheckTodoUnique(Todo todo)
    {
        var existsTodo = await _context.Todos.Where(t => t.Title == todo.Title && t.Category == todo.Category).FirstOrDefaultAsync();
        return existsTodo == null;
    }

    public async Task AddTodo(Todo todo)
    {
        await _context.Todos.AddAsync(todo);
    }

    public async Task DeleteTodo(int id)
    {
        await _context.Todos.Where(t => t.Id == id).ExecuteDeleteAsync();
    }

    public async Task UpdateTodo(Todo todo)
    {
        await _context.Todos.Where(t => t.Id == todo.Id).ExecuteUpdateAsync(t => t
                .SetProperty(p => p.Title, todo.Title)
                .SetProperty(p => p.Category, todo.Category)
                .SetProperty(p => p.Color, todo.Color));
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}