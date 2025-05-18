using System;
using Microsoft.EntityFrameworkCore;
using Task_Collaboration_API.Entities;

namespace Task_Collaboration_API.Data;

public class TaskCollaborationContext(DbContextOptions<TaskCollaborationContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<UserTask> Tasks => Set<UserTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}
