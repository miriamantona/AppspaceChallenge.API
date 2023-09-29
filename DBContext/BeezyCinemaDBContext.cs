using AppspaceChallenge.API.Model.BeezyCinema;
using AppspaceChallenge.API.Responses;
using Microsoft.EntityFrameworkCore;

namespace AppspaceChallenge.API.DBContext
{
  public class BeezyCinemaDBContext : DbContext
  {
    public BeezyCinemaDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Movie>().ToTable("Movie", schema: "dbo");
      modelBuilder.Entity<Cinema>().ToTable("Cinema", schema: "dbo");
      modelBuilder.Entity<Room>().ToTable("Room", schema: "dbo");
      modelBuilder.Entity<Session>().ToTable("Session", schema: "dbo");
    }


    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Session> Sessions { get; set; }

  }
}
