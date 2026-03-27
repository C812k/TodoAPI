using Microsoft.EntityFrameworkCore;
using TodoAPI.AppDataContext;
using TodoAPI.Contracts;
using TodoAPI.Interfaces;
using TodoAPI.Models;

namespace TodoAPI.Services;

public class TodoService : ITodoService
{
    //This is the databse connection. We receive it  through the constuctor (DI)
    private readonly AppDbContext _context;

    //Constructor: .NET automatically injects AppDbContext here (because we registered it in the Program.cs)
    public TodoService(AppDbContext context)
    {
        _context = context;
    }

    //Get all: fetches every todo from the database and converts each one to a TodoResponse.
    public async Task<List<TodoResponse>> GetAllAsync()
    {
        var todos = await _context.Todos.ToListAsync();
        var response = new List<TodoResponse>();

        foreach (var todo in todos)
        {
            response.Add(new TodoResponse
            {
                Id = todo.Id,
                Title = todo.Title,
                IsDone = todo.IsDone,
                CreatedAt = todo.CreatedAt
            });
        }

        return response;
    }

    //GET By ID: Finds a single todo. Returns null if doesn't exist.
    public async Task<TodoResponse?> GetByIdAsync(int id)
    {
       var todo = await _context.Todos.FindAsync(id);

       if(todo == null)
       {
        return null;
       }

       return new TodoResponse
       {
           Id = todo.Id,
           Title = todo.Title,
           IsDone = todo.IsDone,
           CreatedAt = todo.CreatedAt
       };
    }

    //create: builds a new TodoItem from the request
    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request)
    {
        var todo = new TodoItem
        {
            Title = request.Title,
            IsDone = false, // New todos are not done by default
            CreatedAt = DateTime.UtcNow
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return new TodoResponse
        {
            Id = todo.Id,
            Title = todo.Title,
            IsDone = todo.IsDone,
            CreatedAt = todo.CreatedAt
        };
    }

    //update: finds the existing todo, updates its properties, and saves changes. Returns null if the todo doesn't exist.
    public async Task<TodoResponse?> UpdateAsync(int id, CreateTodoRequest request)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return null;
        }

        todo.Title = request.Title;
        await _context.SaveChangesAsync();

        return new TodoResponse
        {
            Id = todo.Id,
            Title = todo.Title,
            IsDone = todo.IsDone,
            CreatedAt = todo.CreatedAt
        };
    }

    //delete: finds the todo and removes it from the database. Returns false if not found, true if deleted.
    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if(todo == null)
        {
            return false;
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return true;
    }
}