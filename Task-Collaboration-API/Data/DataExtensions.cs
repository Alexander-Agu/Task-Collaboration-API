using System;
using Microsoft.EntityFrameworkCore;

namespace Task_Collaboration_API.Data;

public static class DataExtensions
{
    public async static Task MigrateDbAsync(this WebApplication app)
    {
        var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TaskCollaborationContext>();
        await dbContext.Database.MigrateAsync();
    }
}
