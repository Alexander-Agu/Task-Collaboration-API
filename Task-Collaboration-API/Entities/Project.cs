using System;

namespace Task_Collaboration_API.Entities;

public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateOnly CreatedDate { get; set; }

    // Realtionships
    public int UserId { get; set; }
    public User? User { get; set; }

    public List<User>? Collaborators { get; set; }
    public List<UserTask>? Tasks { get; set; }
}
