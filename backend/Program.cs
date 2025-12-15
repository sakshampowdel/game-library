using backend.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<GameDto> games = [
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

// GET /games
app.MapGet("games", () => games);

// GET /games/{id}
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id))
  .WithName(GetGameEndpointName);

// POST /games
app.MapPost("games", (CreateGameDto newGame) =>
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
app.MapPut("games/{id}", (int id, UpdateGameDto updatedGame) =>
{
  int index = games.FindIndex(game => game.Id == id);

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
app.MapDelete("games/{id}", (int id) =>
{
  games.RemoveAll(game => game.Id == id);
  
  return Results.NoContent();
});

app.Run();
