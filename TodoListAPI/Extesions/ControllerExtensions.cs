using Microsoft.AspNetCore.Mvc;
using Serilog;
using TodoListAPI.Extesions;

namespace TodoListAPI.Extesions
{
    public class ControllerExtensions: ControllerBase, IControllerExtension
    {
        
        public ActionResult SaveChangesHandler(int resultValue)
        {
            if (resultValue > 0)
            {
                Log.Warning("Сhanges are saved to the database successfully");
                return Ok();
            }
            else
            {
                Log.Warning("Task was not added, submitted task is null");
                return BadRequest("Сhanges are not saved to the database");
            }
        }
    }
}
