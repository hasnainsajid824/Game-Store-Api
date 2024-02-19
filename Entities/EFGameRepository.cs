using gamestoreapi.Data;
using gamestoreapi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace gamestoreapi.Entities;

public class EFGameRepository : IGameRepository
{
    private readonly GameStoreContext dbContext;

    public EFGameRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }
    public async Task<Game?> GetAsync(int id)
    {
        return await dbContext.Games.FindAsync(id);
    }
    public async Task createAsync(Game game)
    {
        dbContext.Games.Add(game);
        await dbContext.SaveChangesAsync();
    }
    public async Task updateAsync(Game updateGame)
    {
        dbContext.Update(updateGame);
        await dbContext.SaveChangesAsync();
    }
    public async Task deleteAsync(int id)
    {
        await dbContext.Games.Where(game => game.Id == id)
                        .ExecuteDeleteAsync();
    }
}