using TodoListDB.Entities;

namespace TodoListAPI.Model
{
    public class NewMainTask : IDescription
    {
        public string Description { get; set; }

        public NewMainTask(string description)
        {
            this.Description = description;
        }
    }
}
