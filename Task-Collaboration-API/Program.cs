using Microsoft.EntityFrameworkCore;
using Task_Collaboration_API.Data;
using Task_Collaboration_API.Endpoints;

var builder = WebApplication.CreateBuilder(args);


var connString = builder.Configuration.GetConnectionString("TaskCollab");
builder.Services.AddSqlite<TaskCollaborationContext>(connString);
var app = builder.Build();


await app.MigrateDbAsync();

app.MapUserEndpoints();


app.Run();
