using PAW3.Data.Repositories;

namespace PAW.Data.Repositories;

public interface IRepositoryUser
{
    Task<bool> UpsertAsync(Users entity, bool isUpdating);
    Task<bool> CreateAsync(Users entity);
    Task<bool> DeleteAsync(Users entity);
    Task<IEnumerable<Users>> ReadAsync();
    Task<Users> FindAsync(int id);
    Task<bool> UpdateAsync(Users entity);
    Task<bool> UpdateManyAsync(IEnumerable<Users> entities);
    Task<bool> ExistsAsync(Users entity);
}
public class RepositoryUser : RepositoryBase<Users>, IRepositoryUser
{
}
