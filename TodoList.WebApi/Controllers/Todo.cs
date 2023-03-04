using Microsoft.AspNetCore.Mvc;
using TodoList.DatabaseManager.Interfaces;
using TodoList.Models.Entities;
using TodoList.WebApi.Models;

namespace TodoList.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class TodoController : ControllerBase
{
    private readonly ILogger<TodoController> _logger;
    private readonly IDatabaseManager _databaseManager;
    
    public TodoController(ILogger<TodoController> logger, IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }

    [HttpGet("{id:int?}", Name = "Get")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Todo>>> Get(int? id = null)
    {
        _logger.LogInformation("Method Get");
        List<Todo> result = new();
        try
        {
            if (id.HasValue)
            {
                var todo = await _databaseManager.GetTodo(id.Value, true);
                if (todo != null)
                {
                    result.Add(todo);
                }
            }
            else
            {
                result = await _databaseManager.GetAllTodo(true);
            }
        }
        catch (Exception e)
        {
            _logger.LogError("Method Get error: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        if (result.Count == 0)
        {
            return NotFound();
        }

        return Ok(result);
    }
    
    [HttpPost(Name = "Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> Add([FromBody] TodoApi todoApi)
    {
        _logger.LogInformation("Method Add");
        Todo newTodo = new()
        {
            Title = todoApi.Title,
            Category = todoApi.Category,
            Color = todoApi.Color
        };
        if (!await _databaseManager.CheckTodoUnique(newTodo))
        {
            return Conflict("Todo with same title and category already exists.");
        }

        try
        {
            await _databaseManager.AddTodo(newTodo);
            await _databaseManager.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Method Add error: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        
        return Ok(newTodo.Id);
    }

    [HttpDelete("{id:int}", Name = "Delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        _logger.LogInformation("Method Delete");
        
        if (await _databaseManager.GetTodo(id) == null)
        {
            return NotFound();
        }

        try
        {
            await _databaseManager.DeleteTodo(id);
            await _databaseManager.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Method Delete error: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        
        return Ok();
    }

    [HttpPut("{id:int}/title={title}", Name = "Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Update(int id, string title)
    {
        _logger.LogInformation("Method Update");
        
        var todo = await _databaseManager.GetTodo(id);
        if (todo == null)
        {
            return NotFound();
        }

        try
        {
            todo.Title = title;
            await _databaseManager.UpdateTodo(todo);
            await _databaseManager.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Method Update error: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        
        return Ok();
    }
}