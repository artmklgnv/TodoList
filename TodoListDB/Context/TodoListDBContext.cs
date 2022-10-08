using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoListDB.Entities;

namespace TodoListDB.Context
{
    public class TodoListDBContext: DbContext
    {
        public TodoListDBContext() : base() { }
        public TodoListDBContext(DbContextOptions<TodoListDBContext> option) : base(option) { }
        public DbSet<TodoTask> TodoTasks { get; set; }
    }
}
