namespace gamestoreapi.Dto;

public record GameDto
(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateTime ReleaseDate,
    string ImageUri
);