using System;
using Task_Collaboration_API.Dtos.Project;
using Task_Collaboration_API.Entities;

namespace Task_Collaboration_API.Mapping;

public static class ProjectMapping
{
    public static Project ToEntity(this CreateProject project)
    {
        return new()
        {
            Name = project.Name,
            Description = project.Name,
            CreatedDate = project.CreatedDate
        };
    }


    public static Project ToEntity(this UpdateProject project)
    {
        return new()
        {
            Name = project.Name,
            Description = project.Name,
            CreatedDate = project.CreatedDate
        };
    }


    public static ProjectDto ToDto(this Project project)
    {
        return new(
            Name: project.Name,
            Description: project.Description,
            CreatedDate: project.CreatedDate
        );
    }
}
