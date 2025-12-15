using System;
using backend.Dtos;

namespace backend.Endpoints;

public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";

  private static readonly List<GameDto> games = [
    new (
      1,
      "Civilization VI",
      "4X",
      59.99m,
      new DateOnly(2016, 10, 21)
      ),
    new (
      2,
      "Stellaris",
      "4X",
      39.99m,
      new DateOnly(2016, 5, 9)
      ),
    new (
      3,
      "Cities: Skylines",
      "Management",
      29.99m,
      new DateOnly(2015, 3, 10)
      ),
    new (
      4,
      "Planet Zoo",
      "Management",
      44.99m,
      new DateOnly(2019, 11, 5)
      ),
    new (
      5,
      "Endless Legend",
      "4X",
      29.99m,
      new DateOnly(2014, 9, 18)
      )
  ];

  public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
  {
    var group = app.MapGroup("games").WithParameterValidation();

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/{id}
    group.MapGet("/{id}", (int id) =>
    {
      GameDto? game = games.Find(game => game.Id == id);
      
      return game is null ? Results.NotFound() : Results.Ok(game);
    })
    .WithName(GetGameEndpointName);


    // POST /games
    group.MapPost("/", (CreateGameDto newGame) =>
    {
      GameDto game = new (
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
      );

      games.Add(game);

      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
    });
    
    // PUT /games
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
    {
      int index = games.FindIndex(game => game.Id == id);

      if (index == -1)
      {
        return Results.NotFound();
      }

      games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
      );

      return Results.NoContent();
    });

    // DELETE games/{id}
    group.MapDelete("/{id}", (int id) =>
    {
      games.RemoveAll(game => game.Id == id);
      
      return Results.NoContent();
    });

    return group;
  }
}
