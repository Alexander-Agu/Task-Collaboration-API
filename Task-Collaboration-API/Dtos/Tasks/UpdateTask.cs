using System.ComponentModel.DataAnnotations;

namespace Task_Collaboration_API.Dtos.Tasks;

public record class UpdateTask(
    [Required][StringLength(15)] string Title,
    [Required][StringLength(30)] string Content
);
