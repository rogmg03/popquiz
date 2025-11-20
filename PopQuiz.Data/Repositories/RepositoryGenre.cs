using PAW.Data.Models;
using PopQuiz.Data.Models;

namespace PAW.Data.Repositories;

public interface IRepositoryGenre
{
    Task<bool> UpsertAsync(Genre entity, bool isUpdating);
    Task<bool> CreateAsync(Genre entity);
    Task<bool> DeleteAsync(Genre entity);
    Task<IEnumerable<Genre>> ReadAsync();
    Task<Genre> FindAsync(int id);
    Task<bool> UpdateAsync(Genre entity);
    Task<bool> UpdateManyAsync(IEnumerable<Genre> entities);
    Task<bool> ExistsAsync(Genre entity);
}
public class RepositoryGenre : RepositoryBase<Genre>, IRepositoryGenre
{
    public RepositoryGenre(HallOfFameContext context) : base(context)
    {
    }
}
