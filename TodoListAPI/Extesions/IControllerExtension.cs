using Microsoft.AspNetCore.Mvc;

namespace TodoListAPI.Extesions
{
    public interface IControllerExtension
    {
        public ActionResult SaveChangesHandler(int resultValue);
    }
}
