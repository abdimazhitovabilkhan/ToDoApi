using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.DTOs;
using ToDoApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Подключение EF Core + SQLite
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=todo.db"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.SuppressModelStateInvalidFilter = false;
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// Автоматически применяем миграции
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TodoContext>();
    db.Database.Migrate();
}

// API Endpoints
app.MapGet("/api/todo", async (
    TodoContext db,
    IMapper mapper,
    bool? isComplete,
    string? sort
) =>
{
    IQueryable<TodoItem> query = db.TodoItems;

    if (isComplete.HasValue)
    {
        query = query.Where(t => t.IsComplete == isComplete.Value);
    }

    query = sort switch
    {
        "title" => query.OrderBy(t => t.Title),
        "created" => query.OrderBy(t => t.CreatedAt),
        _ => query.OrderBy(t => t.Id)
    };

    var todos = await query.ToListAsync();
    var result = mapper.Map<List<TodoItemDTO>>(todos);
    return Results.Ok(result);
});

app.MapGet("/api/todo/{id}", async (int id, TodoContext db) =>
    await db.TodoItems.FindAsync(id)
        is TodoItem todo
            ? Results.Ok(todo)
            : Results.NotFound());

app.MapPost("/api/todo", async (CreateTodoDTO dto, TodoContext db, IMapper mapper) =>
{
    if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length < 3 || dto.Title.Length > 100)
    {
        return Results.BadRequest("Title must be between 3 and 100 characters.");
    }

    var todo = mapper.Map<TodoItem>(dto);
    db.TodoItems.Add(todo);
    await db.SaveChangesAsync();

    var result = mapper.Map<TodoItemDTO>(todo);
    return Results.Created($"/api/todo/{todo.Id}", result);
});

app.MapPut("/api/todo/{id}", async (int id, TodoItem inputTodo, TodoContext db) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Title = inputTodo.Title;
    todo.IsComplete = inputTodo.IsComplete;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/todo/{id}", async (int id, TodoContext db) =>
{
    var todo = await db.TodoItems.FindAsync(id);
    if (todo is null) return Results.NotFound();

    db.TodoItems.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok(todo);
});

app.Run();
