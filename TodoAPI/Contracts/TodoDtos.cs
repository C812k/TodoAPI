namespace TodoAPI.Contracts;

//What client  send when creating a new todo
public class CreateTodoRequest
{
    public string Title { get; set; } = "";
    
}

//What the API sends back to the client 

public class TodoResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
}   