using System.ComponentModel.DataAnnotations;

namespace Task_Collaboration_API.Dtos.Tasks;

public record class TaskDto(
    string Title,
    string Content,
    DateOnly CreatedDate
);