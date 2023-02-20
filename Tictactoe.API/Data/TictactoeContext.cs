using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tictactoe.API.Models;

namespace Tictactoe.API.Data;   

public class TictactoeContext : DbContext
{
    public TictactoeContext()
    {
    }
    
    public TictactoeContext(DbContextOptions<TictactoeContext> options) : base(options)
    {
    }
    
    public DbSet<Room> GameRooms { get; set; }
    public DbSet<PlayerInfo> PlayerInfo { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //Database.EnsureCreated();
    }
}