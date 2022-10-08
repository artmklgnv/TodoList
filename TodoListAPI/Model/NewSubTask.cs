using TodoListDB.Entities;

namespace TodoListAPI.Model
{
    public class NewSubTask : IDescription
    {
        public string Description { get; set; }
        public int ParentId { get; set; }

        public NewSubTask(string description, int parentId)
        {
            this.Description = description;
            this.ParentId = parentId;
        }
    }
}
