using Microsoft.AspNetCore.Mvc;
using TodoList.DatabaseManager.Interfaces;
using TodoList.Models.Entities;

namespace TodoList.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IDatabaseManager _databaseManager;
    
    public TodoController(ILogger<TodoController> logger, IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    [HttpGet(Name = "GetAll")]
    public IEnumerable<Todo> Get()
    {
        _logger.LogError("test");
        return _databaseManager.GetAllTodo();
    }
}