using Microsoft.EntityFrameworkCore;
using Task_Collaboration_API.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connString = builder.Configuration.GetConnectionString("TaskCollab");
builder.Services.AddSqlServer<TaskCollaborationContext>(connString);

await app.MigrateDbAsync();

app.Run();
