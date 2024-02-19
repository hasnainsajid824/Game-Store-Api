using System.Text.RegularExpressions;
using gamestoreapi.Dto;
using gamestoreapi.Entities;
using gamestoreapi.Repositories;

namespace gamestoreapi.Endpoints;

public static class GameEndpoints
{   
    const string GetGameEndpoint = "GetGame";
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        // Get All
        group.MapGet("", async (IGameRepository repository) => (await repository.GetAllAsync())
        .Select(game=> game.AsDto()));

        // Get by ID
        group.MapGet("/{id}", async (IGameRepository repository, int id)=>{
            Game? game = await repository.GetAsync(id);
            return game is not null? Results.Ok(game.AsDto()): Results.NotFound();
            }).WithName(GetGameEndpoint);


        // Post
        group.MapPost("", async (IGameRepository repository, CreateGameDto gameDto)=>
        {
            Game? game = new(){
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };
            await repository.createAsync(game);
            return Results.CreatedAtRoute(GetGameEndpoint, new{id = game.Id}, game);
        });

        // Put
        group.MapPut("/{id}",async (IGameRepository repository, int id, UpdateGameDto updatedGameDto)=>{
            Game? game = await repository.GetAsync(id);
            if (game is null){
                return Results.NotFound();
            }
            
            game.Name = updatedGameDto.Name;
            game.Price = updatedGameDto.Price;
            game.ReleaseDate = updatedGameDto.ReleaseDate;
            game.ImageUri = updatedGameDto.ImageUri;
            game.Genre = updatedGameDto.Genre;

            await repository.updateAsync(game);
            return Results.NoContent();
            
            });

        // Delete
        group.MapDelete("/{id}",async (IGameRepository repository, int id) => {
            Game? game = await repository.GetAsync(id);

            if (game is not null){
                await repository.deleteAsync(id);
            }
            
            return Results.NoContent();
            });
        return group;
        }


}