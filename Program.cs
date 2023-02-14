using Data;
using Features.Users.Requests;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors();

builder.Services.AddDbContext<DataDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

app.MapGet("/users", async (DataDbContext context, CancellationToken cancellationToken) =>
{
    return await context.Users.ToListAsync(cancellationToken);
})
.WithName("GetAllUsers")
.WithOpenApi();

app.MapPost("/users", async (AddUserRequest addUserRequest, DataDbContext context, CancellationToken cancellationToken) =>
{
    await context.Users.AddAsync(new() { Title = addUserRequest.Title }, cancellationToken);
    await context.SaveChangesAsync(cancellationToken);
})
.WithName("AddUser")
.WithOpenApi();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();

// Here is the migration executed
dbContext.Database.Migrate();

app.Run();
