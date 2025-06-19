using System.ComponentModel.DataAnnotations;

namespace ToDoApi.DTOs;

public class CreateTodoDTO
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Title { get; set; }

    public bool IsComplete { get; set; }
}
