using PAW3.Data.Repositories;

namespace PAW.Data.Repositories;

public interface IRepositoryDirector
{
    Task<bool> UpsertAsync(Director entity, bool isUpdating);
    Task<bool> CreateAsync(Director entity);
    Task<bool> DeleteAsync(Director entity);
    Task<IEnumerable<Director>> ReadAsync();
    Task<Director> FindAsync(int id);
    Task<bool> UpdateAsync(Director entity);
    Task<bool> UpdateManyAsync(IEnumerable<Director> entities);
    Task<bool> ExistsAsync(Director entity);
}
public class RepositoryDirector : RepositoryBase<Directors>, IRepositoryDirector
{
}
