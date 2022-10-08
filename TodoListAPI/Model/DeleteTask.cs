using TodoListDB.Entities;

namespace TodoListAPI.Model
{
    public class DeleteTask : IEntity
    {
        public int Id { get; set; }
        public DeleteTask(int id)
        {
            this.Id = id;
        }
    }
}
