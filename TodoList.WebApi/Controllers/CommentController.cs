using Microsoft.AspNetCore.Mvc;
using TodoList.DatabaseManager.Interfaces;
using TodoList.Models.Entities;

namespace TodoList.WebApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CommentController : ControllerBase
{
    private readonly ILogger<CommentController> _logger;
    private readonly IDatabaseManager _databaseManager;
    
    public CommentController(ILogger<CommentController> logger, IDatabaseManager databaseManager)
    {
        _logger = logger;
        _databaseManager = databaseManager;
    }
    
    [HttpGet("{id:int}", Name = "GetByTodo")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Comment>>> GetByTodo(int id)
    {
        _logger.LogInformation("Method GetByTodo");
        List<Comment>? result;
        try
        {
            var todo = await _databaseManager.GetTodo(id, true);
            if (todo == null)
            {
                return NotFound();
            }

            result = todo.Comments;

        }
        catch (Exception e)
        {
            _logger.LogError("Method GetByTodo error: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }

        return Ok(result);
    }
    
    [HttpPost(Name = "AddComment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> AddComment([FromBody] Comment comment)
    {
        _logger.LogInformation("Method AddComment");
        var todo = await _databaseManager.GetTodo(comment.TodoId);
        if (todo == null)
        {
            return NotFound();
        }

        try
        {
            await _databaseManager.AddComment(comment);
            await _databaseManager.SaveChanges();
        }
        catch (Exception e)
        {
            _logger.LogError("Method AddComment error: {Message}", e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
        
        return Ok(comment.Id);
    }
}