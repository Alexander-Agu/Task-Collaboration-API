using System.ComponentModel.DataAnnotations;

namespace Task_Collaboration_API.Dtos.Project;

public record class CreateProject(
    [Required] string Name,
    [Required] string Description
);