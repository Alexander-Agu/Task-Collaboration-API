using System;

namespace Task_Collaboration_API.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

public class TaskCollaborationContextFactory : IDesignTimeDbContextFactory<TaskCollaborationContext>
{
    public TaskCollaborationContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<TaskCollaborationContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        optionsBuilder.UseSqlServer(connectionString);

        return new TaskCollaborationContext(optionsBuilder.Options);
    }
}