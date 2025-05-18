using System;
using Task_Collaboration_API.Dtos.Tasks;
using Task_Collaboration_API.Entities;

namespace Task_Collaboration_API.Mapping;

public static class TaskMapping
{
    public static UserTask ToEntity(this CreateTask task)
    {
        return new()
        {
            Title = task.Title,
            Content = task.Content,
            CreatedDate = DateOnly.FromDateTime(DateTime.Now)
        };
    }


    public static UserTask ToEntity(this UpdateTask task)
    {
        return new()
        {
            Title = task.Title,
            Content = task.Content,
        };
    }


    public static TaskDto ToDto(this UserTask task)
    {
        return new(
            Title: task.Title,
            Content: task.Content,
            CreatedDate: task.CreatedDate
        );
    }
}
