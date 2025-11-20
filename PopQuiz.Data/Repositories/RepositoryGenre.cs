using PAW3.Data.Repositories;

namespace PAW.Data.Repositories;

public interface IRepositoryGenre
{
    Task<bool> UpsertAsync(Genres entity, bool isUpdating);
    Task<bool> CreateAsync(Genres entity);
    Task<bool> DeleteAsync(Genres entity);
    Task<IEnumerable<Genres>> ReadAsync();
    Task<Genres> FindAsync(int id);
    Task<bool> UpdateAsync(Genres entity);
    Task<bool> UpdateManyAsync(IEnumerable<Genres> entities);
    Task<bool> ExistsAsync(Genres entity);
}
public class RepositoryGenre : RepositoryBase<Genres>, IRepositoryGenre
{
}
