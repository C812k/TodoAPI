using TodoAPI.Contracts;

namespace TodoAPI.Interfaces;

 // This interface defines WHAT our Todo service can do.
 // Any class that implements this interface MUST provide all these methods.
 // The Controller will depend on this interface, not on the actual class —
 // that's Dependency Injection (DI).

 public interface ITodoService
{
    //Returns a list of all todo items
    Task<List<TodoResponse>> GetAllAsync();

    //Retruns a single todo by its id, or null if not found
    Task<TodoResponse?> GetByIdAsync(int id);

    //Creates a new todo item based on the provided request data and returns the created item
    Task<TodoResponse> CreateAsync(CreateTodoRequest request);

    //Updates an existing todo, Returns the updates ttodo or null if not found
    Task<TodoResponse?> UpdateAsync(int id, CreateTodoRequest request);

    //Deletes a todo. Returns true id deleted , false if not found;
    Task<bool> DeleteAsync(int id);
}