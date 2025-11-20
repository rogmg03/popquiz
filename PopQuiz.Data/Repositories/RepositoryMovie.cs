using PAW3.Data.Repositories;

namespace PAW.Data.Repositories;

public interface IRepositoryMovie
{
    Task<bool> UpsertAsync(Movies entity, bool isUpdating);
    Task<bool> CreateAsync(Movies entity);
    Task<bool> DeleteAsync(Movies entity);
    Task<IEnumerable<Movies>> ReadAsync();
    Task<Movies> FindAsync(int id);
    Task<bool> UpdateAsync(Movies entity);
    Task<bool> UpdateManyAsync(IEnumerable<Movies> entities);
    Task<bool> ExistsAsync(Movies entity);
}
public class RepositoryMovie : RepositoryBase<Movies>, IRepositoryMovie
{
}
