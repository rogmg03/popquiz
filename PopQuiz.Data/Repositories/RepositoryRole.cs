using PAW3.Data.Repositories;

namespace PAW.Data.Repositories;

public interface IRepositoryRole
{
    Task<bool> UpsertAsync(Roles entity, bool isUpdating);
    Task<bool> CreateAsync(Roles entity);
    Task<bool> DeleteAsync(Roles entity);
    Task<IEnumerable<Roles>> ReadAsync();
    Task<Roles> FindAsync(int id);
    Task<bool> UpdateAsync(Roles entity);
    Task<bool> UpdateManyAsync(IEnumerable<Roles> entities);
    Task<bool> ExistsAsync(Roles entity);
}
public class RepositoryRole : RepositoryBase<Roles>, IRepositoryRole
{
}
