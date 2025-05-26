using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task_Collaboration_API.Data;
using Task_Collaboration_API.Dtos.Tasks;
using Task_Collaboration_API.Entities;
using Task_Collaboration_API.Mapping;

namespace Task_Collaboration_API.Endpoints;

public static class TasksEndpoints
{
    public static RouteGroupBuilder MapTaskEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/project/task");
        string getTaskEndpoint = "GetTask";


        // GET - get all project tasks by project ID
        group.MapGet("/{projectId}", async (int projectId, TaskCollaborationContext dbContext) =>
        {
            TaskDto[] tasks = await dbContext.Tasks.Where(x => x.ProjectId == projectId)
            .Select(tasks => new TaskDto(
                tasks.Title,
                tasks.Content,
                tasks.CreatedDate
            ))
            .ToArrayAsync();

            return Results.Ok(tasks);
        });


        // GET - gets a task by project, user and task ID
        group.MapGet("/{assignedUserId}/{projectId}/{taskId}", async (int assignedUserId, int projectId, int taskId, TaskCollaborationContext dbContext) =>
        {
            UserTask? task = await dbContext.Tasks
            .Where(x => x.AssignedUserId == assignedUserId && x.ProjectId == projectId && x.Id == taskId)
            .FirstOrDefaultAsync();

            if (task is null) return Results.NotFound();

            return Results.Ok(task.ToDto());
        })
        .WithName(getTaskEndpoint);


        // POST - create a task using assignedUser and project ID
        group.MapPost("/{userId}/{projectsId}", async (int userId, int projectsId, [FromBody] CreateTask newTask, [FromServices] TaskCollaborationContext dbContext) =>
        {
            UserTask? userTask = newTask.ToEntity();
            userTask.AssignedUserId = userId;
            userTask.ProjectId = projectsId;

            await dbContext.Tasks.AddAsync(userTask);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(getTaskEndpoint, new { assignedUserId = userId, projectId = projectsId, taskId = userTask.Id }, userTask.ToDto());
        });


        // PUT - updates task info using user, project and task Id
        group.MapPut("{userId}/{projectsId}/{tasksId}", async (int userId, int projectsId, int tasksId, [FromBody] UpdateTask updateTask, [FromServices] TaskCollaborationContext dbContext) =>
        {
            UserTask? task = await dbContext.Tasks
            .Where(x => x.ProjectId == projectsId && x.AssignedUserId == userId && x.Id == tasksId)
            .FirstOrDefaultAsync();

            if (task is null) return Results.NotFound();

            if (updateTask.Title != "") task.Title = updateTask.Title;
            if (updateTask.Content != "") task.Content = updateTask.Content;

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(getTaskEndpoint, new { assignedUserId = userId, projectId = projectsId, taskId = task.Id }, task.ToDto());
        });


        // DELETE - delete task by using user, project and task Id
        group.MapDelete("{userId}/{projectsId}/{tasksId}", async (int userId, int projectsId, int tasksId, TaskCollaborationContext dbContext) =>
        {
            UserTask? task = await dbContext.Tasks
            .Where(x => x.ProjectId == projectsId && x.AssignedUserId == userId && x.Id == tasksId)
            .FirstOrDefaultAsync();
            if (task is null) return Results.NotFound(); //  Checking if the task exists


            await dbContext.Tasks
            .Where(x => x.ProjectId == projectsId && x.AssignedUserId == userId && x.Id == tasksId)
            .ExecuteDeleteAsync();

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });
        return group;
    }
}
