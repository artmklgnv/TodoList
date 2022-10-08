using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListDB.Entities
{
    public interface IParent
    {
        public int? ParentId { get; set; }
    }
}
