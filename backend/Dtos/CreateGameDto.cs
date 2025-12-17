using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record class CreateGameDto(
  [Required][StringLength(50)] string Name,
  int GenreId,
  [Range(0, 1000)] decimal Price,
  DateOnly ReleaseDate
);
