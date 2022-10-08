using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TodoListDB.Entities
{
    public class TodoTask : IEntity, IDescription, IParent
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public List<TodoTask> SubTasks { get; set; } = new List<TodoTask>();
        public bool IsDone { get; set; }
        public TodoTask(string description)
        {
            this.Description = description;
            this.IsDone = false;
        }
    }
}
