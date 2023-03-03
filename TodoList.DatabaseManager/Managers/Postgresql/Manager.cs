using TodoList.DatabaseManager.Interfaces;

namespace TodoList.DatabaseManager.Managers.Postgresql;

public class Manager : IDatabaseManager, IDisposable
{
    private readonly Context _context;

    public Manager(Context context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}