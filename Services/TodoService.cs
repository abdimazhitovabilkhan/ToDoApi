// Services/TodoService.cs
using ToDoApi.Models;

namespace ToDoApi.Services;

public class TodoService
{
    public bool IsValidTitle(string? title)
    {
        return !string.IsNullOrWhiteSpace(title) &&
               title.Length >= 3 &&
               title.Length <= 100;
    }

    public TodoItem Create(string title)
    {
        return new TodoItem
        {
            Title = title,
            IsComplete = false,
            CreatedAt = DateTime.UtcNow
        };
    }
}
