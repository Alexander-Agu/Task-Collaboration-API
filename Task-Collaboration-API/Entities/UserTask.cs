using System;

namespace Task_Collaboration_API.Entities;

public class UserTask
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required DateOnly CreatedDate { get; set; }

    // Relationships
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    public int AssignedUserId { get; set; }
    public User? AssignedUser { get; set; }
}
