namespace Task_Collaboration_API.Dtos.Project;

public record class ProjectDto(
    string Name,
    string Description,
    DateOnly CreatedDate
);