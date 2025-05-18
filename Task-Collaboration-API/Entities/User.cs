using System;

namespace Task_Collaboration_API.Entities;

public class User
{
    public int Id { get; set; }
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required DateOnly CreatedDate { get; set; }

    // Relationships
    public List<Project>? Projects { get; set; }
    public List<Task>? Tasks { get; set; }
}
