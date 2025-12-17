using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public record class UpdateGameDto(
  [Required][StringLength(50)] string Name,
  int GenreId,
  [Range(1, 1000)] decimal Price,
  DateOnly ReleaseDate
);