using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore;
using flashcardApp.Api.Persistence;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(11, 3, 2))));    

builder.Services.AddControllers().AddFluentValidation(fv =>
fv.RegisterValidatorsFromAssembly(Assembly.Load("flashcardApp.Shared")));

builder.Services.AddSwaggerGen();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();