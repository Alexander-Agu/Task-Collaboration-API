using System;
using Microsoft.EntityFrameworkCore;
using Task_Collaboration_API.Data;
using Task_Collaboration_API.Dtos.Project;
using Task_Collaboration_API.Entities;
using Task_Collaboration_API.Mapping;

namespace Task_Collaboration_API.Endpoints;

public static class ProjectEnpoints
{
    public static RouteGroupBuilder MapProjectEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("project");
        string getProjectEndpoint = "GetProject";

        // GET - get all user projects with userId
        group.MapGet("/projects/{Id}", async (int Id, TaskCollaborationContext dbContext) =>
        {
            ProjectDto[] projects = await dbContext.Projects
            .Where(x => x.UserId == Id)
            .Select(project => new ProjectDto(
                project.Name,
                project.Description,
                project.CreatedDate
            )).ToArrayAsync();

            return Results.Ok(projects);
        });


        // GET - get a single project by userId and projectId
        group.MapGet("/{userId}/{projectId}", async (int userId, int projectId, TaskCollaborationContext dbContext) =>
        {
            Project? project = await dbContext.Projects
            .Where(x => userId == x.UserId && projectId == x.Id)
            .FirstOrDefaultAsync();

            if (project is null) return Results.NotFound();

            return Results.Ok(project.ToDto());
        })
        .WithName(getProjectEndpoint);


        // POST - create project using userId
        group.MapPost("/{id}", async (int id, CreateProject newProject, TaskCollaborationContext dbContext) =>
        {
            // CHecking if project already exists
            bool projectExists = await dbContext.Projects.Where(x => x.Name == newProject.Name).AnyAsync();
            if (projectExists) return Results.Conflict(new { message = "Project already exists" });

            Project project = newProject.ToEntity();
            project.UserId = id;

            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(getProjectEndpoint, new { userId = id, projectId = project.Id }, project.ToDto());
        })
        .WithParameterValidation();


        // PUT - update project data
        group.MapPut("/{usersId}/{projectsId}", async (int usersId, int projectsId, UpdateProject updateProject, TaskCollaborationContext dbContext) =>
        {
            Project? project = await dbContext.Projects.Where(x => x.Id == projectsId && x.UserId == usersId).FirstOrDefaultAsync();
            if (project is null) return Results.NotFound();


            if (updateProject.Name != "" && !string.IsNullOrEmpty(project.Name))
            {
                // Checking if project already exists
                bool projectExists = await dbContext.Projects.Where(x => x.Name == updateProject.Name).AnyAsync();
                if (projectExists) return Results.Conflict(new { message = "Project already exists" });
                else
                {
                    project.Name = updateProject.Name;
                }
            }

            if (updateProject.Description != "" && !string.IsNullOrEmpty(project.Description)) project.Description = updateProject.Description;

            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(getProjectEndpoint, new { userId = usersId, projectId = projectsId }, project.ToDto());
        });


        // DELETE - delete projects by user and project id
        group.MapDelete("/{usersId}/{projectsId}", async (int usersId, int projectsId, TaskCollaborationContext dbContext) =>
        {
            Project? project = await dbContext.Projects.Where(x => x.UserId == usersId && x.Id == projectsId).FirstOrDefaultAsync();
            if (project is null) Results.NotFound();

            await dbContext.Projects.Where(x => x.UserId == usersId && x.Id == projectsId).ExecuteDeleteAsync();
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        return group;
    }
}
