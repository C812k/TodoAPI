using Microsoft.EntityFrameworkCore;
using TodoAPI.Models;

namespace TodoAPI.AppDataContext
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions <AppDbContext> options) : base(options)
        {
            
        }
        public DbSet<TodoItem> Todos => Set<TodoItem>();
    }
}