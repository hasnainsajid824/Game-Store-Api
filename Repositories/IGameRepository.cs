using gamestoreapi.Entities;

namespace gamestoreapi.Repositories;

public interface IGameRepository
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetAsync(int id);
    Task createAsync(Game game);
    Task deleteAsync(int id);
    Task updateAsync(Game updateGame);
};
