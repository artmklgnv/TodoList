using Microsoft.EntityFrameworkCore;
using TodoListAPI.Extesions;
using TodoListAPI.Settings;
using TodoListDB.Context;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IControllerExtension , ControllerExtensions>();
SQLiteSettings sqlSettings = builder.Configuration.GetSection("SQLite").Get<SQLiteSettings>();
builder.Services.AddDbContext<TodoListDBContext>(
    option => option.UseSqlite(sqlSettings.ConnectionString),
    contextLifetime: ServiceLifetime.Singleton, 
    optionsLifetime: ServiceLifetime.Singleton);

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(builder.Environment.ContentRootPath, "log.log"))
    .CreateBootstrapLogger();
builder.Host.UseSerilog();

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build =>
{
    build.WithOrigins("http://127.0.0.1:5173").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
