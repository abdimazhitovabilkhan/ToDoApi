using AutoMapper;
using ToDoApi.Models;
using ToDoApi.DTOs;

namespace ToDoApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, TodoItemDTO>();
            CreateMap<CreateTodoDTO, TodoItem>();
        }
    }
}

