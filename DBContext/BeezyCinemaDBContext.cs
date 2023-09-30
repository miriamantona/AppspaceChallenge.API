using AppspaceChallenge.API.Model.BeezyCinema;
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
      modelBuilder.Entity<MovieGenre>().ToTable("MovieGenre", schema: "dbo");
      modelBuilder.Entity<Genre>().ToTable("Genre", schema: "dbo");

      modelBuilder.Entity<Movie>()
        .HasMany(m => m.MovieGenres)
        .WithOne(mg => mg.Movie)
        .HasForeignKey(mg => mg.MovieId);

      modelBuilder.Entity<MovieGenre>()
          .HasKey(mg => new { mg.MovieId, mg.GenreId });
    }


    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<Genre> Genres { get; set; }
  }
}
