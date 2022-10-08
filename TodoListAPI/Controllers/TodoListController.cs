using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TodoListAPI.Extesions;
using TodoListAPI.Model;
using TodoListDB.Context;
using TodoListDB.Entities;

namespace TodoListAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoListDBContext _context;
        private IControllerExtension _extension;
        public TodoListController(TodoListDBContext context, IControllerExtension extension)
        {
            _context = context;
            _extension = extension;
        }

        [HttpPost("addSubTask")]
        public async Task<ActionResult> AddSubTask(NewSubTask newTask)
        {
            if (newTask == null)
            {
                Log.Warning("Task was not added, submitted task is null");
                return BadRequest("Task was not added, submitted task is null");
            }

            TodoTask? parentTask = _context.TodoTasks.Include(t => t.SubTasks).SingleOrDefault(taks => taks.Id == newTask.ParentId);
            if (parentTask != null)
            {
                parentTask.SubTasks.Add(new TodoTask(newTask.Description));
                int result = _context.SaveChanges();
                return _extension.SaveChangesHandler(result);
            }
            else
            {
                Log.Warning($"There is no task with this parent id: {newTask.ParentId} in the database. Task '{newTask.Description}' will not be added.");
                return BadRequest($"There is no task with this parent id: {newTask.ParentId} in the database. Task will not be added.");
            }
        }

        [HttpPost("addMainTask")]
        public async Task<ActionResult> AddMainTask(NewMainTask newTask)
        {
            if (newTask == null)
            {
                Log.Warning("Task was not added, submitted task is null");
                return BadRequest("Task was not added, submitted task is null");
            }

            _context.TodoTasks.Add(new TodoTask(newTask.Description));
            var result = _context.SaveChanges();
            return _extension.SaveChangesHandler(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<TodoTask>>> GetMainTasks()
        {
            List<TodoTask>? todoTasks = _context.TodoTasks.ToList();
            if (todoTasks != null && todoTasks.Count > 0)
            {
                return Ok(todoTasks);
            }
            else
            {
                Log.Information("Can not get tasks. DB is empty");
                return NotFound("Can not get tasks. DB is empty");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<TodoTask>>> GetTaskById(int id)
        {
            TodoTask? todoTasks = _context.TodoTasks.SingleOrDefault(taks => taks.Id == id);
            if (todoTasks != null)
            {
                Log.Information($"The task was found successfully");
                return Ok(todoTasks);
            }
            else
            {
                Log.Information($"No task with id: {id} in DB");
                return NotFound($"No task with id: {id} in DB");
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateTask(UpdateTask updateTask)
        {
            if (updateTask == null)
            {
                Log.Warning($"Task was not updated, submitted task is null");
                return BadRequest("Task was not updated, submitted task is null");
            }

            TodoTask? taskToUpdate = _context.TodoTasks.SingleOrDefault(task => task.Id == updateTask.Id);

            if (taskToUpdate != null)
            {
                taskToUpdate.Description = updateTask.Description;
                taskToUpdate.IsDone = updateTask.IsDone;
                int result = _context.SaveChanges();
                return _extension.SaveChangesHandler(result);
            }
            else
            {
                Log.Warning($"There is no task with this id: {updateTask.Id} in the database. Task will not be updated.");
                return NotFound($"There is no task with this id: {updateTask.Id} in the database. Task will not be updated.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask(int id)
        {
            if (id < 0)
            {
                Log.Warning($"Task was not deleted, id: {id} is not correct");
                return BadRequest($"Task was not deleted, id: {id} is not correct");
            }

            TodoTask? taskToRemove = _context.TodoTasks.SingleOrDefault(task => task.Id == id);

            if (taskToRemove != null)
            {
                foreach (TodoTask subTask in taskToRemove.SubTasks)
                {
                    _context.TodoTasks.Remove(subTask);
                } 
                _context.TodoTasks.Remove(taskToRemove);
                int result = _context.SaveChanges();
                return _extension.SaveChangesHandler(result);
            }
            else
            {
                Log.Warning($"There is no task with this id: {id} in the database. Task will not be deleted.");
                return NotFound($"There is no task with this id: {id} in the database. Task will not be deleted.");
            }
        }
    }
}
