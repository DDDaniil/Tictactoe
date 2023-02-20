using Microsoft.EntityFrameworkCore;
using Tictactoe.API.Data;
using Tictactoe.API.Game;
using Tictactoe.API.Hubs;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; 

builder.Services.AddDbContext<TictactoeContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<Game>();

builder.Services.AddSignalR(); 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policyName",
        policyBuilder =>
        {
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("policyName");

app.UseAuthorization();

app.MapHub<GameHub>("/game");

app.Run();