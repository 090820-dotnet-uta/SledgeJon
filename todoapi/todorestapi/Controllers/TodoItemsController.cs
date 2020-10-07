using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todorestapi.Models;

namespace todorestapi.Controllers
{
  [Route("api/TodoItems")]
  [ApiController]
  public class TodoItemsController : Controller
  {
    private readonly TodoDbContext _context;

    public TodoItemsController(TodoDbContext context)
    {
      _context = context;
    }

    // GET: api/TodoItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
    {
      return await _context.TodoItems.ToListAsync();
    }

    // GET: api/TodoItems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDTO>> GetTodoItem(int id)
    {
      var todoItemDTO = await _context.TodoItems.FindAsync(id);

      if (todoItemDTO == null)
      {
        return NotFound();
      }

      return todoItemDTO;
    }

    // PUT: api/TodoItems/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutTodoItem(int id, TodoItemDTO todoItemDTO)
    {
      if (id != todoItemDTO.TodoItemDTOId)
      {
        return BadRequest();
      }

      _context.Entry(todoItemDTO).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!TodoItemExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/TodoItems
    // To protect from overposting attacks, enable the specific properties you want to bind to, for
    // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
    [HttpPost]
    public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
    {
      _context.TodoItems.Add(todoItemDTO);
      await _context.SaveChangesAsync();

      return todoItemDTO;
      //return CreatedAtAction(nameof(GetTodoItem), new { TodoItemDTOId = todoItemDTO.TodoItemDTOId }, todoItemDTO);
    }

    // DELETE: api/TodoItems/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<TodoItemDTO>> DeleteTodoItem(int id)
    {
      var todoItemDTO = await _context.TodoItems.FindAsync(id);
      if (todoItemDTO == null)
      {
        return NotFound();
      }

      _context.TodoItems.Remove(todoItemDTO);
      await _context.SaveChangesAsync();

      return todoItemDTO;
    }

    private bool TodoItemExists(int id)
    {
      return _context.TodoItems.Any(e => e.TodoItemDTOId == id);
    }
  }
}
