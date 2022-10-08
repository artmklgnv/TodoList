using TodoListDB.Entities;

namespace TodoListAPI.Model
{
    public class UpdateTask : IEntity, IDescription, IStatus
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
        public UpdateTask(int id, string description, bool isDone)
        {
            this.Id = id;
            this.Description = description;
            IsDone = isDone;
        }
    }
}
