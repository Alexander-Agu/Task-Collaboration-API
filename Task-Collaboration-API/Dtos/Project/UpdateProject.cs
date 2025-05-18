using System.ComponentModel.DataAnnotations;

namespace Task_Collaboration_API.Dtos.Project;

public record class UpdateProject(
    [Required][StringLength(15)] string Name,
    [Required][StringLength(50)] string Description
);