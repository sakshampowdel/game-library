using System;
using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options)
  : DbContext(options)
{
  public DbSet<Game> Games => Set<Game>();
  public DbSet<Genre> Genres => Set<Genre>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Genre>().HasData(
      new { Id = 1, Name = "4X" },
      new { Id = 2, Name = "Management" },
      new { Id = 3, Name = "Kids and Family" }
    );
  }
}
