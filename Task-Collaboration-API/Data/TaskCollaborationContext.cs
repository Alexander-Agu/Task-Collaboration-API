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
        modelBuilder.Entity<Project>().HasOne(u => u.User)
        .WithMany(p => p.Projects)
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Cascade);

        // Many-to-many: Project collaborators
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Collaborators)
            .WithMany()
            .UsingEntity(j => j.ToTable("ProjectCollaborators"));
    }
}
