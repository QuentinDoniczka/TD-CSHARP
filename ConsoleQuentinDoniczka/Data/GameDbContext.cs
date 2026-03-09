using ConsoleQuentinDoniczka.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleQuentinDoniczka.Data;

public class GameDbContext : DbContext
{
    public DbSet<GameRecord> GameRecords { get; set; }
    public DbSet<GameSave> GameSaves { get; set; }

    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    {
    }
}
