using System;

namespace Task_Collaboration_API.Entities;

public class Task
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateOnly CreatedDate { get; set; }

    // Relationships
    public required int ProjectId { get; set; }
    public Project? Project { get; set; }

    public required int AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
}
